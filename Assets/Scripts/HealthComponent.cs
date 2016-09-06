using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Manage the health of a pawn
 */
public class HealthComponent : AComponent {

    // Link to the UI (for the player)
    public HealthUI healthUI;

    // Health variables
    public int maxHealth;
    int currentHealth;

    // Duration of the knockedDownDuration
    public float knockedDownDuration;

    // States of the character
    bool bDead;
    bool bKnockedDown;

    // GETTERs
    public bool IsDead() { return bDead; }
    public bool IsKnockedDown() { return bKnockedDown; }

    protected override void Start()
    {
        base.Start();

        if (healthUI)
            healthUI.healthSlider.maxValue = maxHealth;
        currentHealth = maxHealth;
    }

    /**
     * Main function of this class
     */
    public void TakeDamage(int damageTaken)
    {
        // If the character is dead or knockedDown, he is immune to damage
        if (bDead || bKnockedDown)
            return;

        // Taking damage interupt any action or future action
        pawn.controller.ResetNextInput();

        if (pawn is PawnPlayer)
        {
            PlayerController playerController = pawn.controller as PlayerController;
            playerController.Interupt();
        }

        pawn.SetStatus(APawn.EStatus.IDLE);
        pawn.GetAnimator().Play("TakeDamage");

        currentHealth -= damageTaken;
        if (healthUI)
            healthUI.SetSlider(maxHealth - currentHealth);

        if (currentHealth <= 0 && !bDead)
            Die();
    }

    public void KnockedDown()
    {
        if (pawn.GetHealth().IsDead())
            return;
        pawn.GetAnimator().Play("KnockedDown");
        bKnockedDown = true;
        Invoke("GetUp", knockedDownDuration);
    }

    void GetUp()
    {
        pawn.GetAnimator().SetTrigger("trigger_getUp");
    }

    void KnockDownOver()
    {
        bKnockedDown = false;
    }    

    public void Die()
    {
        pawn.GetAnimator().Play("Die");
        bDead = true;
    }
}
