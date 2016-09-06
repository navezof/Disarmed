using UnityEngine;
using System.Collections;

/**
 * Debug class, used to quickly test some features
 */
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
