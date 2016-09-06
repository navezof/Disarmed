using UnityEngine;
using System.Collections;
using System;

/**
 * A leaf is a final child of a branch of a behaviour tree it is an action
 * 
 */
public abstract class ALeaf : ANode {

    public abstract override EState Run();
}
