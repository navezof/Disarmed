using UnityEngine;
using System.Collections;

public class Lef_Move : ALeaf {

    public Transform target;
    public float stoppingDistance;

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
