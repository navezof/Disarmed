using UnityEngine;
using System.Collections;

/**
 * A decorator function as a kind of condition for the node.
 * 
 * Every decorator has a Try function which has to be implemented.
 * 
 */
public abstract class ADecorator : MonoBehaviour {

    // Controller owner, set by the associated ANode
    protected AIController controller;

    // If true, the decorator result will be inverted
    public bool bInvert;

    // Return true if the condition are met
    public abstract bool Try();

    public void SetController(AIController newController) { controller = newController; }
}
