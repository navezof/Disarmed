using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class APawn : MonoBehaviour {

    public AController controller;

    protected Animator animator;
    protected HealthComponent health;    

    public Animator GetAnimator() { return animator; }
    public HealthComponent GetHealth() { return health; }

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<HealthComponent>();
    }
}