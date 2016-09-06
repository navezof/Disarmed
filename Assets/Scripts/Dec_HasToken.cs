using UnityEngine;
using System.Collections;
using System;

/**
 * Check if the character has an attack token
 */
public class Dec_HasToken : ADecorator
{
    public override bool Try()
    {
        if (controller == null)
            return false;
        if (!controller.HasToken())
            return false;
        return true;
    }
}
