using UnityEngine;
using System.Collections;

public class AttackComponent : AComponent {

    APawn target;

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
                print("Dodged!");
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
}
