using UnityEngine;
using System.Collections;

public class DashComponent : AComponent {

    public void Dash(APawn target)
    {
        print("dash on " + target.name);
    }

    public APawn FindTarget(Vector3 playerPosition, Vector3 swipeEnd)
    {
        return SwarmController.GetSwarmController().GetAllEnemies()[0];
    }
}
