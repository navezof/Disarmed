using UnityEngine;
using System.Collections;
using System;

/**
 * Controller specificly used by the AI
 */
public class AIController : AController
{
    // Possessed pawn
    PawnAI pawn;

    // Behaviour tree used by this AI
    public ANode behaviourTreeRoot;

    // Attack token variable
    public bool bToken;
    public bool HasToken() { return bToken; }

    // The decision tick allow an AI to pause between every tree climb-through
    public float decisionTick;
    float lastDecisionTick; 

    // Take possession of a pawn
    public override void Possess(APawn pawnAI)
    {
        pawn = pawnAI as PawnAI;
    }

    // The update will launch a run of the behaviourtree root, which in turn trigger the run for its children, and so on...
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

    // A bToken value at true means the enemy can attack
    public void TakeToken()
    {
        bToken = true;
    }
}
