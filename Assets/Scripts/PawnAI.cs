using UnityEngine;
using System.Collections;

public class PawnAI : APawn {

    float threat;
    public float GetThreat() { return threat; }
    public void AddThreat(float value) { threat += value; }

    public int priorityLevel;
    public bool bHasAttacked;

    protected override void Start()
    {
        base.Start();

        controller = GetComponent<AIController>();
        controller.Possess(this);

        attack.SetTarget(SwarmController.GetSwarmController().GetPlayer());
    }

    public AIController GetController()
    {
        return controller as AIController;
    }
}
