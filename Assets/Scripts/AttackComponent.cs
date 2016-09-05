using UnityEngine;
using System.Collections;

public class AttackComponent : AComponent {

    public enum EAttackRange
    {
        CLOSE,
        MID,
        LONG
    }

    APawn target;
    public APawn GetTarget() { return target; }
    public void SetTarget(APawn value) { target = value; }

    public int currentAttackIndex = 0;
    public int maxAttackIndex = 3;

    public float closeRange = 2.12f;
    public float midRange = 3.51f;
    public float longRange = 6f;

    public int attackDamage;
    public int comboStrikeValue;

    public void Attack(APawn newTarget)
    {
        if (newTarget == null)
            target = SwarmController.GetSwarmController().GetClosestEnemy(pawn);
        else
            target = newTarget;

        print("Attack on " + target.name);

        pawn.SetStatus(APawn.EStatus.ATTACKING);

        transform.LookAt(target.transform);

        pawn.GetAnimator().SetInteger("cAttackIndex", currentAttackIndex);
        pawn.GetAnimator().SetInteger("cRange", GetRange());
        pawn.GetAnimator().SetTrigger("Attack");

        currentAttackIndex++;
        if (currentAttackIndex > maxAttackIndex)
            currentAttackIndex = 0;
    }

    int GetRange()
    {
        float distanceCharacterEnemy = Vector3.Distance(transform.position, target.transform.position);
        if (distanceCharacterEnemy <= closeRange)
            return 0;
        if (distanceCharacterEnemy > closeRange && distanceCharacterEnemy <= midRange)
            return 1;
        if (distanceCharacterEnemy > midRange && distanceCharacterEnemy <= longRange)
            return 2;
        return 0;
    }

    void StrikePoint()
    {
        print("Strike");
        if (target.GetDodge() != null)
        {
            if (target.GetDodge().IsDodging())
            {
                return;
            }
        }
        if (pawn is PawnPlayer)
        {
            PawnPlayer pawnPlayer = pawn as PawnPlayer;
            pawnPlayer.GetCombo().AddCombo(comboStrikeValue);
        }
        target.GetHealth().TakeDamage(attackDamage);
    }

    void KnockDownStrikePoint()
    {
        print("KnockDown");
        target.GetHealth().TakeDamage(attackDamage);
        target.GetHealth().KnockedDown();
        currentAttackIndex = 0;
        pawn.controller.ResetNextInput();
    }

    void LaunchArrow()
    {
    }

    void EndAttack()
    {
        if (pawn.controller.nextInput == PlayerController.EInput.NONE)
            currentAttackIndex = 0;
        pawn.SetStatus(APawn.EStatus.IDLE);
        if (pawn.controller is AIController)
            SwarmController.GetSwarmController().TakeToken();
    }

    public void SetAttackIndex(int attackIndex)
    {
        currentAttackIndex = attackIndex;
    }

    void OpenParry()
    {
        if (target.GetDodge() != null)
            target.GetDodge().SetCanDodge(true);
    }

    void CloseParry()
    {
        if (target.GetDodge() != null)
            target.GetDodge().SetCanDodge(false);
    }

    public float GetRange(EAttackRange range)
    {
        switch (range)
        {
            case EAttackRange.CLOSE:
                return closeRange;
            case EAttackRange.MID:
                return midRange;
            case EAttackRange.LONG:
                return longRange;
            default:
                return -1;
        }
    }
}
