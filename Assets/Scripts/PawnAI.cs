using UnityEngine;
using System.Collections;

/**
 * Pawn class of the AI
 * 
 */
public class PawnAI : APawn {

    // Threat variable
    float threat;
    public float GetThreat() { return threat; }
    public void AddThreat(float value) { threat += value; }

    // Each enemy has a different priority for targeting
    public int priorityLevel;

    // True if the character recently attacked
    public bool bHasAttacked;

    protected override void Start()
    {
        base.Start();

        // Similar to the PawnAI, the controller is possessed here.
        controller = GetComponent<AIController>();
        controller.Possess(this);

        attack.SetTarget(SwarmController.GetSwarmController().GetPlayer());
    }

    public AIController GetController()
    {
        return controller as AIController;
    }
}
