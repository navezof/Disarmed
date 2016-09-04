using UnityEngine;
using System.Collections;

public class DodgeComponent : AComponent {

    public bool bDodging;
    public bool IsDodging() { return bDodging; }

    public bool canDodge;
    public void SetCanDodge(bool value) { canDodge = value; }

    public void Dodge()
    {
        pawn.GetAnimator().Play("Dodge");
        if (canDodge)
        {
            print("Dodge");
            bDodging = true;
        }
        else
        {
            if (pawn is PawnPlayer)
            {
                PawnPlayer pawnPlayer = pawn as PawnPlayer;
                pawnPlayer.GetCombo().ResetCombo();
            }
        }
    }

    void EndAnim()
    {
        bDodging = false;
        pawn.controller.nextInput = AController.EInput.NONE;
    }
}
