using UnityEngine;
using System.Collections;

public class Debug : MonoBehaviour {

    public PawnPlayer player;
    public PawnAI enemy;

	void Update ()
    {
	    if (Input.GetKeyDown("a"))
        {
            player.GetHealth().TakeDamage(1);
        }
        if (Input.GetKeyDown("q"))
        {
            enemy.GetHealth().KnockedDown();
        }
	}
}
