using UnityEngine;
using System.Collections;
using System;

public class Dec_HasToken : ADecorator
{
    public override bool Try()
    {
        if (!controller.HasToken())
            return false;
        return true;
    }
}
