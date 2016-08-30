using UnityEngine;
using System.Collections;

public class PawnPlayer : APawn {

    DashComponent dash;
    AttackComponent attack;
    DodgeComponent dodge;

	protected override void Start()
    {
        base.Start();

        dash = GetComponent<DashComponent>();
        attack = GetComponent<AttackComponent>();
        dodge = GetComponent<DodgeComponent>();
	}
}
