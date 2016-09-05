using UnityEngine;
using System.Collections;

public abstract class ADecorator : MonoBehaviour {

    protected AIController controller;

    public bool bInvert;

    public abstract bool Try();

    public void SetController(AIController newController) { controller = newController; }
}
