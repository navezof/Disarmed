using UnityEngine;
using System.Collections;

public class PawnPlayer : APawn {

    DashComponent dash;
    ComboComponent combo;

    //DodgeComponent dodge;

    public DashComponent GetDash() { return dash; }
    public ComboComponent GetCombo() { return combo; }
    //public DodgeComponent GetDodge() { return dodge; }

    protected override void Start()
    {
        base.Start();
            
        controller = GetComponent<PlayerController>();
        controller.Possess(this);

        dash = GetComponent<DashComponent>();
        combo = GetComponent<ComboComponent>();
        //attack = GetComponent<AttackComponent>();
        //dodge = GetComponent<DodgeComponent>();
	}
}
