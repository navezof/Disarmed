using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwarmController : MonoBehaviour {

    static SwarmController swarmController;
    public static SwarmController GetSwarmController()
    {
        return swarmController;
    }

    public APawn[] pawns;
    List<APawn> menacingEnemies;

    void Awake()
    {
        swarmController = this;
    }

    public APawn[] GetAllEnemies()
    {
        return pawns;
    }

    public List<APawn> GetMenacingEnemies()
    {
        menacingEnemies.Clear();
        return menacingEnemies;
    }

    public APawn GetClosestEnemies(List<APawn> menacingEnemies)
    {
        return menacingEnemies[0];
    }

    public APawn GetClosestEnemies(APawn player)
    {
        APawn bestTarget = pawns[0];
        foreach (APawn pawn in pawns)
        {
            if (Vector3.Distance(pawn.transform.position, player.transform.position) < Vector3.Distance(bestTarget.transform.position, player.transform.position))
            {
                bestTarget = pawn;
            }
        }
        return bestTarget;
    }
}
