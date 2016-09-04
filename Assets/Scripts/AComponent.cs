using UnityEngine;
using System.Collections;

/**
 * Base component class
 * 
 */
public abstract class AComponent : MonoBehaviour {

    // Every component is associated with a pawn
    protected APawn pawn;

    protected virtual void Start()
    {
        pawn = GetComponent<APawn>();
    }
}
