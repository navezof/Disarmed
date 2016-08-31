using UnityEngine;
using System.Collections;

public class PawnPlayer : APawn {

    DashComponent dash;
    AttackComponent attack;
    DodgeComponent dodge;

    public DashComponent GetDash() { return dash; }
    public AttackComponent GetAttack() { return attack; }
    public DodgeComponent GetDodge() { return dodge; }

    protected void Start()
    {
        controller = GetComponent<PlayerController>();
        controller.Possess(this);

        dash = GetComponent<DashComponent>();
        attack = GetComponent<AttackComponent>();
        dodge = GetComponent<DodgeComponent>();
	}
}
