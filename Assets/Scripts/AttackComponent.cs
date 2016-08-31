using UnityEngine;
using System.Collections;

public class AttackComponent : AComponent {

    APawn target;

    int cAttackIndex = 0;

    public void Attack(APawn newTarget)
    {
        if (newTarget == null)
        {
            target = SwarmController.GetSwarmController().GetClosestEnemies(pawn);
        }
        print("Attack on " + target.name);
        //pawn.GetAnimator().Play("Atk_melee_0" + cAttackIndex, 0);
        pawn.GetAnimator().SetTrigger("Attack");
        pawn.GetAnimator().SetInteger("cRange", GetRange());
        pawn.GetAnimator().SetInteger("cAttackIndex", cAttackIndex);

        cAttackIndex++;
    }

    int GetRange()
    {
        return 1;
    }

    void StrikePoint()
    {
    }

    void EndAttack()
    {
    }
}
