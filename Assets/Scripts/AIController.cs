using UnityEngine;
using System.Collections;
using System;

public class AIController : AController
{
    PawnAI pawn;

    public ANode behaviourTreeRoot;

    public bool bToken;
    public bool HasToken() { return bToken; }

    public float decisionTick;
    float lastDecisionTick; 

    public override void Possess(APawn pawnAI)
    {
        pawn = pawnAI as PawnAI;
    }


    void Update()
    {
        if (Time.time - lastDecisionTick > decisionTick)
        {
            if (!pawn.GetHealth().IsDead() && !pawn.GetHealth().IsKnockedDown())
                behaviourTreeRoot.Run();
            lastDecisionTick = Time.time;
        }
    }

    public PawnAI GetPawn()
    {
        return pawn;
    }

    public void TakeToken()
    {
        bToken = true;
    }
}
