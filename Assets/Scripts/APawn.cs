using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class APawn : MonoBehaviour {

    public AController controller;

    protected virtual void Start()
    {
        if (controller != null)
            controller.Possess(this);
    }
}