using UnityEngine;
using System.Collections;

public abstract class AController : MonoBehaviour {

    protected APawn pawn;

    public void Possess(APawn newPawn)
    {
        pawn = newPawn;
    }
}
