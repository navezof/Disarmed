using UnityEngine;
using System.Collections;

public class PawnPlayer : APawn {

    DashComponent dash;
    //DodgeComponent dodge;

    public DashComponent GetDash() { return dash; }
    //public DodgeComponent GetDodge() { return dodge; }

    protected override void Start()
    {
        base.Start();
            
        controller = GetComponent<PlayerController>();
        controller.Possess(this);

        dash = GetComponent<DashComponent>();
        attack = GetComponent<AttackComponent>();
        //dodge = GetComponent<DodgeComponent>();
	}
}
