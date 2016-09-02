using UnityEngine;
using System.Collections;

public class DodgeComponent : AComponent {

    public bool bDodging;
    public bool IsDodging() { return bDodging; }

    bool canDodge;
    public void SetCanDodge(bool value) { canDodge = value; }

    public void Dodge()
    {
        pawn.GetAnimator().Play("Dodge");
        if (canDodge)
        {
            print("Dodge");
            bDodging = true;
        }
    }

    void EndAnim()
    {
        bDodging = false;
        pawn.controller.nextInput = AController.EInput.NONE;
    }
}
