using UnityEngine;
using System.Collections;

public abstract class AController : MonoBehaviour {

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

    public abstract void Possess(APawn pawn);
}
