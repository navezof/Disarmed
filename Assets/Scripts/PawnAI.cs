using UnityEngine;
using System.Collections;

public class PawnAI : APawn {

    float threat;
    public float GetThreat() { return threat; }

    EStatus status;
    public EStatus GetStatus() { return status; }

    public enum EStatus
    {
        IDLE,
        MOVING,
        ATTACKING,
        KNOCKEDDOWN,
        DEAD
    }

    protected override void Start()
    {
        base.Start();

        controller = GetComponent<AIController>();
        controller.Possess(this);
    }
}
