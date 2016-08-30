using UnityEngine;
using System.Collections;

public class DashComponent : AComponent {

    public void Dash(APawn target)
    {
        print("dash on " + target.name);
    }
}
