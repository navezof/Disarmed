using UnityEngine;
using System.Collections;

/**
 * A behaviour tree is composed of several node, with each node havinf from zero to infinite node children
 * 
 */
public abstract class ANode : MonoBehaviour {

    public enum EState
    {
        SUCCESS,
        FAILURE,
        RUNNING,
        ERROR
    }

    // Controller currently controlling this node
    public AIController controller;
    public void SetController(AIController value) { controller = value; }

    // Each node as zero or more decorator, which are conditions. If a decorator fail, the node won't launch
    protected ADecorator[] decorators;

    // Each node as zero or more children
    public ANode[] childrens;

    //
	protected virtual void Start ()
    {
        // We are setting here the root controller which should have as a parent a AIController, 
        // the other node children of the root will have this value as null
        controller = GetComponentInParent<AIController>();
        // The parent is in charge of assigning the controller to its children
        foreach (ANode child in childrens)
        {
            child.SetController(controller);
        }

        // The node get all the decorators on the gameobject
        decorators = GetComponents<ADecorator>();
        // The node is in charge of assigning the controller to its decorators
        foreach (ADecorator decorator in decorators)
        {
            decorator.SetController(controller);
        }
	}

    /**
     * Each node has to try first, it will go through all its decorators and if one fail, the try is a failure
     */
    public bool Try()
    {
        foreach (ADecorator decorator in decorators)
        {
            if (decorator.Try() == false)
                return false;
        }
        return true;
    }

    /*
     * Every node has to implement a Run function, who is the actual execution of the node (in case of leaves) or another selector or sequence
     */
    public abstract EState Run();
}
