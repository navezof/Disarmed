using UnityEngine;
using System.Collections;
using System;

public class Dec_IsCloseToPlayer : ADecorator {

    public float distance;

    public override bool Try()
    {
        print("Try : " + name);
        if (Vector3.Distance(controller.transform.position, SwarmController.GetSwarmController().GetPlayer().transform.position) > distance)
            return bInvert ? true : false;
        return bInvert ? false : true;
    }
}
