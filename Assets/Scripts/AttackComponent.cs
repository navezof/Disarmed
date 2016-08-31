using UnityEngine;
using System.Collections;

public class AttackComponent : AComponent {

    APawn target;

    public int currentAttackIndex = 0;
    int maxAttackIndex = 3;

    public float closeRange = 2.12f;
    public float midRange = 3.51f;
    public float longRange = 6f;

    public void Attack(APawn newTarget)
    {
        if (newTarget == null)
            target = SwarmController.GetSwarmController().GetClosestEnemies(pawn);

        print("Attack on " + target.name);
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
    }

    void KnockDownStrikePoint()
    {
        print("KnockDown");
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
}
