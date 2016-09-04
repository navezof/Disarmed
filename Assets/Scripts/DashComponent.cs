using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DashComponent : AComponent {

    public float targetingConeLength;
    public float targetingConeAngle;

    public float speed;

    List<PawnAI> potentialTargets = new List<PawnAI>();
    List<PawnAI> menacingTargets = new List<PawnAI>();

    PawnAI currentTarget;

    public float attackDistance;

    void Update()
    {
        if (currentTarget == null)
            return;
        if (Vector3.Distance(transform.position, currentTarget.transform.position) <= attackDistance)
        {
            EndDash();
            // Done dashing
        }
        else
        {
            transform.LookAt(currentTarget.transform);
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);
        }
    }

    public void Dash(PawnAI target)
    {
        if (target == null)
        {
            EndDash();
            return;
        }
        currentTarget = target;
        print("dash on " + target.name);
    }

    void EndDash()
    {
        pawn.controller.ResetNextInput();
        currentTarget = null;
    }

    public PawnAI FindTarget(Vector3 playerPosition, Vector3 swipeEnd)
    {
        FindPotentialTarget(playerPosition, swipeEnd);
        FindMenacingTargets();
        return FindClosestTarget();        
    }

    void FindPotentialTarget(Vector3 playerPosition, Vector3 swipeEnd)
    {
        potentialTargets.Clear();

        Vector3 swipeVector = swipeEnd - Camera.main.WorldToScreenPoint(playerPosition);
        swipeVector.Normalize();
        foreach (PawnAI enemy in SwarmController.GetSwarmController().GetAllEnemies())
        {
            if (IsTargetable(enemy))
            {
                Vector3 enemyVector = Camera.main.WorldToScreenPoint(enemy.transform.position) - Camera.main.WorldToScreenPoint(playerPosition);
                float angle = Vector3.Angle(swipeVector, enemyVector);
                float distance = Vector3.Distance(playerPosition, enemy.transform.position);
                print("angle : " + angle);
                print("distance : " + distance);

                if (angle < targetingConeAngle / 2 && distance < targetingConeLength)
                {
                    print("Add enemy : " + enemy.name);
                    potentialTargets.Add(enemy);
                }
                else
                {
                    print("Didn't add enemy : " + enemy.name);
                }
            }
        }
    }

    void FindMenacingTargets()
    {
        menacingTargets.Clear();

        if (potentialTargets.Count <= 0)
            return;

        menacingTargets.Add(potentialTargets[0]);
        foreach (PawnAI enemy in potentialTargets)
        {
            if (enemy.GetThreat() > menacingTargets[0].GetThreat())
            {
                menacingTargets.Clear();
                menacingTargets.Add(enemy);
            }
            else if (enemy.GetThreat() == menacingTargets[0].GetThreat())
            {
                menacingTargets.Add(enemy);
            }
        }
    }

    PawnAI FindClosestTarget()
    {
        if (menacingTargets.Count <= 0)
            return null;

        PawnAI closest = menacingTargets[0];
        foreach (PawnAI enemy in menacingTargets)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, closest.transform.position))
            {
                closest = enemy;
            }

        }
        currentTarget = closest;
        return closest;
    }

    bool IsTargetable(PawnAI enemy)
    {
        if (enemy.GetHealth().IsDead() || enemy.GetHealth().IsKnockedDown() || enemy == currentTarget)
            return false;
        return true;
    }
}
