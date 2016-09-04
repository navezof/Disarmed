using UnityEngine;
using System.Collections;

/** 
 * Player specific pawn
 * 
 */
public class PawnPlayer : APawn {

    /*
     * Components
     */
    DashComponent dash;
    ComboComponent combo;

    public DashComponent GetDash() { return dash; }
    public ComboComponent GetCombo() { return combo; }

    protected override void Start()
    {
        base.Start();
        
        // By possessing, the controller's pawn is defined as this pawn    
        controller = GetComponent<PlayerController>();
        controller.Possess(this);

        dash = GetComponent<DashComponent>();
        combo = GetComponent<ComboComponent>();
	}
}
