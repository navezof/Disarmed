﻿using UnityEngine;
using System.Collections;
using System;

public class AIController : AController
{
    PawnAI pawn;

    public ANode behaviourTreeRoot;

    public bool bToken;
    public bool HasToken() { return bToken; }

    public override void Possess(APawn pawnAI)
    {
        pawn = pawnAI as PawnAI;
    }

    void Update()
    {
        behaviourTreeRoot.Run();
    }

    public PawnAI GetPawn()
    {
        return pawn;
    }
}
