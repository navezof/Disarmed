using UnityEngine;
using System.Collections;

/**
 * This leaf is an action, it make the enemey attack the player
 * 
 */
public class Lef_Attack : ALeaf {

    // Threat value of an attack
    public float threatValue;

    public override EState Run()
    {
        PawnAI aiPawn = controller.GetPawn() as PawnAI;
        controller.GetPawn().GetAttack().Attack(SwarmController.GetSwarmController().GetPlayer());
        controller.bToken = false;
        controller.GetPawn().bHasAttacked = true;
        return EState.SUCCESS;
    }
}
