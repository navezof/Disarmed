using UnityEngine;
using System.Collections;

/**
 * The attack component is in charge of managing the attack and dealing damages to targets
 * 
 */
public class AttackComponent : AComponent {
    
    // There is three range of attack
    public enum EAttackRange
    {
        CLOSE,
        MID,
        LONG
    }

    // Current target of the attack
    APawn target;
    public APawn GetTarget() { return target; }
    public void SetTarget(APawn value) { target = value; }

    // The attackIndex determine which animation to use.
    // This index is used by the Animator to choose the correct animation
    public int currentAttackIndex = 0;
    public int maxAttackIndex = 3;

    // Values of range
    public float closeRange = 2.12f;
    public float midRange = 3.51f;
    public float longRange = 6f;

    // Damage of an attack
    public int attackDamage;
    // Number of combo point added for a succesful attack
    public int comboStrikeValue;

    /**
     * Main function of this class
     * 
     */
    public void Attack(APawn newTarget)
    {
        // The target should be passed to the function, but if the target is null, the closest one to the player is requested from the Swarmcontroller
        if (newTarget == null)
            target = SwarmController.GetSwarmController().GetClosestEnemy(pawn);
        else
            target = newTarget;

        // Setting the status of the pawn
        pawn.SetStatus(APawn.EStatus.ATTACKING);
        // If the pawn is an AI, the threat level of this AI is incrased
        if (pawn is PawnAI)
        {
            PawnAI ai = pawn as PawnAI;
            ai.AddThreat(1);
        }

        transform.LookAt(target.transform);

        // Animator values are set 
        pawn.GetAnimator().SetInteger("cAttackIndex", currentAttackIndex);
        pawn.GetAnimator().SetInteger("cRange", GetRange());
        pawn.GetAnimator().SetTrigger("Attack");

        // In order to launch different animation, the animation index is modified here. If it exceed the max number of attack it goes back to zero
        currentAttackIndex++;
        if (currentAttackIndex > maxAttackIndex)
            currentAttackIndex = 0;
    }

    /**
     * Check the distance between the player and the target and return 0 to 2 depending on the distance
     * 
     */
    int GetRange()
    {
        float distanceCharacterEnemy = Vector3.Distance(transform.position, target.transform.position);
        if (distanceCharacterEnemy <= closeRange)
            return 0;
        if (distanceCharacterEnemy > closeRange && distanceCharacterEnemy <= midRange)
            return 1;
        if (distanceCharacterEnemy > midRange && distanceCharacterEnemy <= longRange)
            return 2;
        return 0;
    }

    public float GetRangeValue(EAttackRange range)
    {
        switch (range)
        {
            case EAttackRange.CLOSE:
                return closeRange;
            case EAttackRange.MID:
                return midRange;
            case EAttackRange.LONG:
                return longRange;
            default:
                return -1;
        }
    }


    /**
     * Actual strike. Damage are dealt, dodge are tried and combo are increased
     */
    void StrikePoint()
    {
        // If the target has a dodge component and is actually dodging the strike is canceled
        if (target.GetDodge() != null)
        {
            if (target.GetDodge().IsDodging())
                return;
        }
        // If the pawn is a player, the combo are added to its comboMeter
        if (pawn is PawnPlayer)
        {
            PawnPlayer pawnPlayer = pawn as PawnPlayer;
            pawnPlayer.GetCombo().AddCombo(comboStrikeValue);
        }
        // Damage are dealt
        target.GetHealth().TakeDamage(attackDamage);
    }

    // A knockdown is launched every four attack, it deals normal damage but the target is knockeddown for a while
    void KnockDownStrikePoint()
    {
        target.GetHealth().TakeDamage(attackDamage);
        target.GetHealth().KnockedDown();
        currentAttackIndex = 0;
        pawn.controller.ResetNextInput();
    }

    // Called at the end of an animation
    void EndAttack()
    {
        if (pawn.controller.nextInput == PlayerController.EInput.NONE)
            currentAttackIndex = 0;
        pawn.SetStatus(APawn.EStatus.IDLE);
        if (pawn.controller is AIController)
        {
            PawnAI ai = pawn as PawnAI;
            ai.AddThreat(-1);
            SwarmController.GetSwarmController().TakeToken();            
        }
    }

    public void SetAttackIndex(int attackIndex)
    {
        currentAttackIndex = attackIndex;
    }

    /*
     * The following function are called by the animation event and open and close the dodge possibility
     */
    void OpenParry()
    {
        if (target.GetDodge() != null)
            target.GetDodge().SetCanDodge(true);
    }

    void CloseParry()
    {
        if (target.GetDodge() != null)
            target.GetDodge().SetCanDodge(false);
    }

    void LaunchArrow()
    {
        // Not implemented
    }

}
