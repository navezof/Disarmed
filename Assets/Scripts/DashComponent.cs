using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Dash functions
 * 
 */
public class DashComponent : AComponent {

    /**
     * Targeting cone value
     */
    public float targetingConeLength;
    public float targetingConeAngle;

    // speed of the dash
    public float speed;

    // This list 
    List<PawnAI> potentialTargets = new List<PawnAI>();
    List<PawnAI> menacingTargets = new List<PawnAI>();

    // Target for the dash
    PawnAI currentTarget;

    // Distance of attack, the dash will stop when arriving at this distance from its target
    public float attackDistance;

    // During the update, we check first if a dash is currently going (currentTarget not null) then if the dash is ended or not
    void Update()
    {
        if (currentTarget == null)
            return;
        if (Vector3.Distance(transform.position, currentTarget.transform.position) <= attackDistance)
        {
            EndDash();
        }
        else
        {
            transform.LookAt(currentTarget.transform);
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);
        }
    }

    // We set the target of the dash
    public void Dash(PawnAI target)
    {
        if (target == null)
        {
            EndDash();
            return;
        }
        currentTarget = target;
    }

    // Once the dash is ended the player directly launch an attack;
    void EndDash()
    {
        pawn.GetAttack().Attack(currentTarget);
        pawn.controller.ResetNextInput();
        currentTarget = null;
    }

    /*
     * The most important function of this class, the selection of a valid target
     */
    public PawnAI FindTarget(Vector3 playerPosition, Vector3 swipeEnd)
    {
        FindPotentialTarget(playerPosition, swipeEnd);
        FindMenacingTargets();
        return FindClosestTarget();        
    }

    /**
     * When looking for a target, we first check all the possible/targetables enemies
     */
    void FindPotentialTarget(Vector3 playerPosition, Vector3 swipeEnd)
    {
        // First emptying list previously made
        potentialTargets.Clear();

        // To make it simpler, all the position are replace in the screen
        Vector3 swipeVector = swipeEnd - Camera.main.WorldToScreenPoint(playerPosition);
        swipeVector.Normalize();

        // We are asking the swarmcontroller (who knows all the enemies) a list of enemies and iterate through it
        foreach (PawnAI enemy in SwarmController.GetSwarmController().GetAllEnemies())
        {
            if (IsTargetable(enemy))
            {
                // Like earlier, we convert the world point to screen point
                Vector3 enemyVector = Camera.main.WorldToScreenPoint(enemy.transform.position) - Camera.main.WorldToScreenPoint(playerPosition);
                // We calculate the angle between the swipe and the enemy...
                float angle = Vector3.Angle(swipeVector, enemyVector);
                // the distance between them...
                float distance = Vector3.Distance(playerPosition, enemy.transform.position);
                // and if the angle is between half a cone, and withing distance, a potential target is added to the list
                if (angle < targetingConeAngle / 2 && distance < targetingConeLength)
                {
                    potentialTargets.Add(enemy);
                }
            }
        }
    }

    /**
     * Once the possible targets have been identified, we pick the most menacing one (with the higher threat level)
     * 
     */
    void FindMenacingTargets()
    {
        // Like potentialTargets, emptying of the list previously made
        menacingTargets.Clear();

        // If no potential target are found, we return here
        if (potentialTargets.Count <= 0)
            return;

        // On this loop, we iterate through our list in order to find the most menacing targets
        menacingTargets.Add(potentialTargets[0]);
        foreach (PawnAI enemy in potentialTargets)
        {
            // If enemy's threat is greater than the currently stored target, we clear the whole list, and add enemy
            if (enemy.GetThreat() > menacingTargets[0].GetThreat())
            {
                menacingTargets.Clear();
                menacingTargets.Add(enemy);
            }
            // if enemy threat is equal to the stored enemy, we simply add this enemy
            else if (enemy.GetThreat() == menacingTargets[0].GetThreat())
            {
                menacingTargets.Add(enemy);
            }
        }
    }

    /**
     * Last part of the selection process, from all the enemies selected, we chose the closest one
     * 
     */
    PawnAI FindClosestTarget()
    {
        // Like earlier, if the list is empty, return here
        if (menacingTargets.Count <= 0)
            return null;

        // Here a simpler loop, with a check on the distance of each menacing target
        PawnAI closest = menacingTargets[0];
        foreach (PawnAI enemy in menacingTargets)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, closest.transform.position))
                closest = enemy;
        }
        currentTarget = closest;
        return closest;
    }

    // This function return false if the enemy is for any reason not targetable
    bool IsTargetable(PawnAI enemy)
    {
        if (enemy.GetHealth().IsDead() || enemy.GetHealth().IsKnockedDown() || enemy == currentTarget)
            return false;
        return true;
    }
}
