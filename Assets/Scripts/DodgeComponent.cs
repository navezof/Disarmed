using UnityEngine;
using System.Collections;

public class DodgeComponent : AComponent {

    bool bDodging;
    public bool GetDodging() { return bDodging; }

    public void Dodge()
    {
        print("Dodge");
        pawn.GetAnimator().Play("Dodge");            
        bDodging = true;
    }

    void EndAnim()
    {
        bDodging = false;
        pawn.controller.nextInput = AController.EInput.NONE;
    }
}
