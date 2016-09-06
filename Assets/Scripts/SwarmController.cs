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
    public PawnAI[] enemies;

    // Player pawn
    public PawnPlayer player;

    // The token is given to enemies, an enemy with a token will be able to attack
    bool bToken = true;
    public float timeBetweenAttack;

    // List of attackers
    List<PawnAI> readyAttackers = new List<PawnAI>();
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

    void Start()
    {
        player = GameObject.Find("PLAYER").GetComponent<PawnPlayer>();
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

    /**
     * To get the next attacker, several check are made
     */
    PawnAI GetNextAttacker()
    {
        GetReadyAttackers();
        GetVisibleAttackers();
        GetPriorityAttackers();
        nextAttacker = GetClosestEnemy(priorityAttackers);        
        return nextAttacker;
    }

    /**
     * We first check if at least one enemy hasn't already attacked, if no enemies are ready, they are all reset
     */
    List<PawnAI> GetReadyAttackers()
    {
        readyAttackers.Clear();
        foreach (PawnAI enemy in enemies)
        {
            if (!enemy.bHasAttacked)
                readyAttackers.Add(enemy);
        }
        if (readyAttackers.Count <= 0)
        {
            ResetReadiness();
        }
        return readyAttackers;
    }

    /**
     * The second check is to select the visible enemies.
     * 
     */
    List<PawnAI> GetVisibleAttackers()
    {
        visibleAttackers.Clear();
        foreach (PawnAI enemy in readyAttackers)
        {
            if (IsVisibleOnCamera(enemy.transform))
                visibleAttackers.Add(enemy);
        }
        return visibleAttackers;
    }

    /**
     * As each enemy has different priority level, the one with the most priority his chosen to attack next
     */
    List<PawnAI> GetPriorityAttackers()
    {
        int bestPriorityLevel = 0;
        priorityAttackers.Clear();
        // In this loop, if the current attacker has the same priority level as the best one, he is added to the list
        // If superior, the list is cleard, and the current attacker is added instead
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
        return priorityAttackers;
    }

    /**
     * Check if the enemy is in the viewport
     */
    bool IsVisibleOnCamera(Transform enemy)
    {
        if (Camera.main.WorldToViewportPoint(enemy.position).x >= 0 && Camera.main.WorldToViewportPoint(enemy.position).x <= 1
            && Camera.main.WorldToViewportPoint(enemy.position).y >= 0 && Camera.main.WorldToViewportPoint(enemy.position).y <= 1
            && Camera.main.WorldToViewportPoint(enemy.position).z > 0)
            return true;
        return false;
    }

    /**
     * Loop through all the enemies and return the closest one to the player
     */
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

    /**
     * Loop through a list of potential target, and find the one the closest to the player
     */
    public PawnAI GetClosestEnemy(List<PawnAI> potentialAttackers)
    {
        if (potentialAttackers.Count <= 0)
        {
            if (enemies.Length > 0)
                return null;
            return enemies[0];
        }
        PawnAI bestTarget = potentialAttackers[0];
        foreach (PawnAI pawn in potentialAttackers)
        {
            if (Vector3.Distance(pawn.transform.position, player.transform.position) < Vector3.Distance(bestTarget.transform.position, player.transform.position))
                bestTarget = pawn;
        }
        return bestTarget;
    }
   
    void ResetReadiness()
    {
        foreach (PawnAI enemy in enemies)
            enemy.bHasAttacked = false;
    }

    public APawn[] GetAllEnemies()
    {
        return enemies;
    }

    public PawnPlayer GetPlayer()
    {
        return player;
    }
}
