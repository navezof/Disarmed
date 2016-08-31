using UnityEngine;
using System.Collections;

public class AttackComponent : AComponent {

    APawn target;

    public void Attack(APawn newTarget)
    {
        if (newTarget == null)
        {
            target = SwarmController.GetSwarmController().GetClosestEnemies(pawn);
        }
        print("Attack on " + target.name);
    }
}
