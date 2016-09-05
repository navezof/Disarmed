using UnityEngine;
using System.Collections;

/**
 * Base class for a controller
 * 
 */
public abstract class AController : MonoBehaviour {

    // Next input to be executed
    public EInput nextInput;
    public enum EInput
    {
        NONE,
        ATTACK,
        DODGE,
        DASH
    }

    public void ResetNextInput()
    {
        nextInput = EInput.NONE;        
    }

    // Set the pawn variable of the controller
    public abstract void Possess(APawn pawn);
}
