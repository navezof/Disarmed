using UnityEngine;
using System.Collections;

/**
 * Action leaf, make the enemy move toward the target
 * 
 */
public class Lef_Move : ALeaf {

    // The target the character has to move to
    // Note that in this version, the target is set manually in the editor
    // Ideally the target should be set by the SwarmController, as a position to go, not the player himself
    public Transform target;
    public float stoppingDistance;

    // During this run, the enemy is moving toward its target until reaching the stoppingDistance
    public override EState Run()
    {
        if (target == null)
            return EState.ERROR;
        if (Vector3.Distance(target.position, controller.transform.position) > stoppingDistance)
        {
            controller.GetPawn().GetMove().MoveTo(target);
            return EState.RUNNING;
        }
        return EState.SUCCESS;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
