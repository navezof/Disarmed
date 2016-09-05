using UnityEngine;
using System.Collections;

public abstract class ANode : MonoBehaviour {

    public enum EState
    {
        SUCCESS,
        FAILURE,
        RUNNING,
        ERROR
    }

    public AIController controller;
    public void SetController(AIController value) { controller = value; }

    protected ADecorator[] decorators;

    public ANode[] childrens;

	protected virtual void Start ()
    {
        controller = GetComponentInParent<AIController>();
        decorators = GetComponents<ADecorator>();
        foreach (ADecorator decorator in decorators)
        {
            decorator.SetController(controller);
        }
        foreach (ANode child in childrens)
        {
            child.SetController(controller);
        }
	}

    public bool Try()
    {
        foreach (ADecorator decorator in decorators)
        {
            if (decorator.Try() == false)
                return false;
        }
        return true;
    }

    public abstract EState Run();
}
