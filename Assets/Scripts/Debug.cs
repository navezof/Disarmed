using UnityEngine;
using System.Collections;

public class Debug : MonoBehaviour {

    public PawnPlayer player;

	void Update ()
    {
	    if (Input.GetKeyDown("a"))
        {
            player.GetHealth().TakeDamage(1);
        }
	}
}
