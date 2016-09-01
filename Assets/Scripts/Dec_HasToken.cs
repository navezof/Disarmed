using UnityEngine;
using System.Collections;
using System;

public class Dec_HasToken : ADecorator
{
    public override bool Try()
    {
        print("Try : " + name);
        if (!controller.HasToken())
            return false;
        return true;
    }
}
