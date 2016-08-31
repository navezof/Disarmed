using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class APawn : MonoBehaviour {

    public AController controller;

    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }
}