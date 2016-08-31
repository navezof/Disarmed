using UnityEngine;
using System.Collections;

public abstract class AComponent : MonoBehaviour {

    protected APawn pawn;

    protected void Start()
    {
        pawn = GetComponent<APawn>();
    }
}
