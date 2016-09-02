using UnityEngine;
using System.Collections;

public class Lef_Move : ALeaf {

    public Transform target;

    public override void Run()
    {
        controller.GetPawn().GetMove().MoveTo(target);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
