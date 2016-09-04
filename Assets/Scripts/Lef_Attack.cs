using UnityEngine;
using System.Collections;

public class Lef_Attack : ALeaf {

    public override void Run()
    {
        controller.GetPawn().GetAttack().Attack(SwarmController.GetSwarmController().GetPlayer());
        controller.bToken = false;
        controller.GetPawn().bHasAttacked = true;
    }
}
