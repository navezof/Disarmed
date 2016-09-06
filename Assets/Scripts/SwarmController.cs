﻿using UnityEngine;
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

    // The token is given to enemies, an enemy with a token will be able to attack
    bool bToken = true;
    public float timeBetweenAttack;

    // List of attackers
    List<PawnAI> visibleAttackers = new List<PawnAI>();
    List<PawnAI> priorityAttackers = new List<PawnAI>();

    // Next attacker who will receive the attack token
    PawnAI nextAttacker;

    /*
     * During the awake, we are Invoking the GiveAttackToken function, and set it to repeat every timeBetweenAttack seconds
     */
    void Awake()
    {
        swarmController = this;
        InvokeRepeating("GiveAttackToken", timeBetweenAttack, timeBetweenAttack);
    }

    /**
     * Iteration through the attackers, and gift of attack token
     * 
     */
    void GiveAttackToken()
    {
        if (bToken == false)
            return;
        if (GetNextAttacker() != null)
        {
            nextAttacker.GetController().TakeToken();
            nextAttacker = null;
            bToken = false;
        }
    }

    /**
     * Once a character did its attack, he will give back
     */
    public void TakeToken()
    {
        bToken = true;
    }

    PawnAI GetNextAttacker()
    {
        // First, only the attackers visible in the camera are valid
        visibleAttackers.Clear();
        foreach (PawnAI enemy in enemies)
        {
            if (IsVisibleOnCamera(enemy.transform))
                visibleAttackers.Add(enemy);
        }
        // Second, we sort enemies by their priority
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
        // Finaly, we check if the potential attacker has already attacked recently
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
                bestTarget = pawn;
        }
        return bestTarget;
    }
}
