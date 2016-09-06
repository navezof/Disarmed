using UnityEngine;
using System.Collections;
using System;

public class Dec_TargetInRange : ADecorator {

    public AttackComponent.EAttackRange range;

    public override bool Try()
    {
        if (controller == null)
            return false;
        AttackComponent attack = controller.GetPawn().GetAttack();
        if (attack == null)
            return bInvert ? true : false;
        if (Vector3.Distance(controller.transform.position, attack.GetTarget().transform.position) > attack.GetRangeValue(range))
            return bInvert ? true : false;
        return bInvert ? false : true;
    }
}
