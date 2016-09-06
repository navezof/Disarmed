using UnityEngine;
using System.Collections;
using System;

/**
 * The sequence is a form similar to the selector, but instead of returning success as soon as an action succeed, 
 * all the children must return success for the sequence to be a success
 * 
 */
public class Sequence : ANode
{
    public override EState Run()
    {
        foreach (ANode child in childrens)
        {
            if (child.Try() == false)
                return EState.FAILURE;
            else if (child.Run() != EState.SUCCESS)
                return EState.FAILURE;
        }
        return EState.SUCCESS;
    }
}
