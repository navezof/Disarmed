using UnityEngine;
using System.Collections;

/**
 * Player specific controller class
 * 
 * Record the input and execute the appropriate action
 * 
 */
public class PlayerController : AController {

    // Pawn possessed by this controller
    PawnPlayer pawn;

    /**
     * Swipe variables
     */
    public float minSwipeInputLength;
    Vector3 swipeStart;
    Vector3 swipeEnd;
    PawnAI target;

    public PawnAI GetTarget() { return target; }

    /**
     * Next input variables
     */
    bool bOpenBuffer;

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
                target = pawn.GetDash().FindTarget(transform.position, swipeEnd);
                StoreInput(EInput.DASH);
            }
            else
            {
                TouchInput(swipeEnd);
            }
        }
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
        else if (nextInput == EInput.NONE)
        {
            nextInput = newInput;
            ExecuteInput();
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
            case EInput.NONE:
                break;
            default:
                print("Input unknown");
                break;
        }
    }

    void OpenBuffer()
    {
        bOpenBuffer = true;
        nextInput = EInput.NONE;
    }

    void CloseBuffer()
    {
        bOpenBuffer = false;
        if (nextInput != EInput.NONE)
            ExecuteInput();
    }
}
