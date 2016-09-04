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
    public PawnPlayer player;

    public float timeBetweenAttack;

    List<PawnAI> visibleAttackers = new List<PawnAI>();
    List<PawnAI> priorityAttackers = new List<PawnAI>();
    PawnAI nextAttacker;

    void Awake()
    {
        swarmController = this;
        InvokeRepeating("GiveAttackToken", timeBetweenAttack, timeBetweenAttack);
    }

    void GiveAttackToken()
    {
        if (GetNextAttacker() != null)
        {
            nextAttacker.GetController().TakeToken();
            nextAttacker = null;
        }
    }

    PawnAI GetNextAttacker()
    {
        visibleAttackers.Clear();
        foreach (PawnAI enemy in pawns)
        {

            {
                visibleAttackers.Add(enemy);
            }
        }
        int bestPriorityLevel = 0;
        priorityAttackers.Clear();
        foreach (PawnAI visibleAttacker in visibleAttackers)
        {
            if (visibleAttacker.priorityLevel > bestPriorityLevel)
            {
                bestPriorityLevel = visibleAttacker.priorityLevel;
                priorityAttackers.Clear();
                priorityAttackers.Add(visibleAttacker);
            }
            if (visibleAttacker.priorityLevel == bestPriorityLevel)
            {
                priorityAttackers.Add(visibleAttacker);
            }
        }
        foreach (PawnAI potentialAttacker in priorityAttackers)
        {
            if (!potentialAttacker.bHasAttacked)
            {
                nextAttacker = potentialAttacker;
            }
        }
        if (nextAttacker == null)
        {
            nextAttacker = priorityAttackers[0];
        }
        return nextAttacker;
    }

    bool IsVisibleOnCamera(Transform enemy)
    {
        if (Camera.main.WorldToViewportPoint(enemy.position).x >= 0 && Camera.main.WorldToViewportPoint(enemy.position).x <= 1
            && Camera.main.WorldToViewportPoint(enemy.position).y >= 0 && Camera.main.WorldToViewportPoint(enemy.position).y <= 1
            && Camera.main.WorldToViewportPoint(enemy.position).z > 0)
            return true;
        return false;
    }

    void Start()
    {
        player = GameObject.Find("PLAYER").GetComponent<PawnPlayer>();
    }

    public APawn[] GetAllEnemies()
    {
        return pawns;
    }

    public PawnPlayer GetPlayer()
    {
        return player;
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
