using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Has information and function on all the enemies in the level
 * 
 */
public class SwarmController : MonoBehaviour {

    // Global access to the class
    static SwarmController swarmController;
    public static SwarmController GetSwarmController()
    {
        return swarmController;
    }

    // List of all the enemies, in order to have a better control, those pawns have to be added manually in the editor
    public APawn[] enemies;

    // Player pawn
    public PawnPlayer player;

    void Awake()
    {
        swarmController = this;
    }

    void Start()
    {
        player = GameObject.Find("PLAYER").GetComponent<PawnPlayer>();
    }

    public APawn[] GetAllEnemies()
    {
        return enemies;
    }

    public PawnPlayer GetPlayer()
    {
        return player;
    }

    public APawn GetClosestEnemy(APawn player)
    {
        APawn bestTarget = enemies[0];
        foreach (APawn pawn in enemies)
        {
            if (Vector3.Distance(pawn.transform.position, player.transform.position) < Vector3.Distance(bestTarget.transform.position, player.transform.position))
            {
                bestTarget = pawn;
            }
        }
        return bestTarget;
    }
}
