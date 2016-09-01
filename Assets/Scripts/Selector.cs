using UnityEngine;
using System.Collections;

public class Selector : ANode {

    public override void Run()
    {
        foreach (ANode child in childrens)
        {
            if (child.Try() == true)
            {
                child.Run();
                return;
            }
        }
    }
}
