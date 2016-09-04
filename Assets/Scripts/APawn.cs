using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Base class for a pawn, hold all the components commonly used
 * 
 */
public abstract class APawn : MonoBehaviour {

    public AController controller;

    /**
     * Components
     * 
     */
    protected Animator animator;
    protected HealthComponent health;
    protected AttackComponent attack;
    protected DodgeComponent dodge;
    protected MoveComponent move;

    /**
     * GETTERs
     */
    public Animator GetAnimator() { return animator; }
    public HealthComponent GetHealth() { return health; }
    public AttackComponent GetAttack() { return attack; }
    public DodgeComponent GetDodge() { return dodge; }
    public MoveComponent GetMove() { return move; }

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<HealthComponent>();
        attack = GetComponent<AttackComponent>();
        dodge = GetComponent<DodgeComponent>();
        move = GetComponent<MoveComponent>();
    }
}