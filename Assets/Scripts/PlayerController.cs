using UnityEngine;
using System.Collections;

public class PlayerController : AController {

    PawnPlayer pawn;

    /**
     * Swipe variables
     */
    public float minSwipeInputLength;
    Vector3 swipeStart;
    Vector3 swipeEnd;
    APawn target;

    /**
     * Next input variables
     */
    bool bOpenBuffer;
    EInput nextInput;
    enum EInput
    {
        NONE,
        ATTACK,
        DODGE,
        DASH
    }

    public override void Possess(APawn pawnPlayer)
    {
        pawn = pawnPlayer as PawnPlayer;
    }

	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            swipeStart = Input.mousePosition;        
        }
        if (Input.GetMouseButtonUp(0))
        {
            swipeEnd = Input.mousePosition;
            if (IsSwipe())
            {
                StoreInput(EInput.DASH);
                target = FindTarget();
            }
            else
            {
                TouchInput(swipeEnd);
            }
        }
        if (!bOpenBuffer && nextInput != EInput.NONE)
        {
            ExecuteInput();
        }
    }

    APawn FindTarget()
    {        
        return GameManager.Instance.GetSwarmController().GetAllEnemies()[0];
    }

    bool IsSwipe()
    {
        if (Vector3.Distance(swipeStart, swipeEnd) > minSwipeInputLength)
            return true;
        return false;
    }

    void TouchInput(Vector3 mousePosition)
    {
        if (mousePosition.x > Screen.width / 2)
        {
            StoreInput(EInput.ATTACK);
        }
        else
        {
            StoreInput(EInput.DODGE);
        }
    }

    void StoreInput(EInput newInput)
    {
        if (bOpenBuffer)
        {
            nextInput = newInput;
        }
    }

    void ExecuteInput()
    {
        switch (nextInput)
        {
            case EInput.ATTACK:
                pawn.GetAttack().Attack(target);
                break;
            case EInput.DODGE:
                pawn.GetDodge().Dodge();
                break;
            case EInput.DASH:
                pawn.GetDash().Dash(target);
                break;
            default:
                print("Input unknown");
                break;
        }
        nextInput = EInput.NONE;
    }

}
