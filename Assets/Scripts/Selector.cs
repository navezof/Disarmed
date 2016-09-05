using UnityEngine;
using System.Collections;

public class Selector : ANode {

    public override EState Run()
    {
        foreach (ANode child in childrens)
        {
            if (child.Try() == true)
            {
                if (child.Run() == EState.SUCCESS)
                    return EState.SUCCESS;
                if (child.Run() == EState.RUNNING)
                    return EState.RUNNING;
            }
        }
        return EState.FAILURE;
    }
}
