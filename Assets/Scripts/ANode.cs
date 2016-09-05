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

    protected AIController controller;
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
