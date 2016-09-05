using UnityEngine;
using System.Collections;
using System;

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
