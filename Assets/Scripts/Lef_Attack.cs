﻿using UnityEngine;
using System.Collections;

public class Lef_Attack : ALeaf {

    public float threatValue;

    public override void Run()
    {
        PawnAI aiPawn = controller.GetPawn() as PawnAI;
        controller.GetPawn().GetAttack().Attack(SwarmController.GetSwarmController().GetPlayer());
    }
}
