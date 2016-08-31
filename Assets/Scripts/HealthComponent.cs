using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthComponent : AComponent {

    public HealthUI healthUI;

    public int maxHealth;
    public int currentHealth;

    bool isDead;
    bool bDamaged;
    bool bKnockedDown;

    protected override void Start()
    {
        base.Start();

        if (healthUI)
            healthUI.healthSlider.maxValue = maxHealth;
    }

    public void TakeDamage(int damageTaken)
    {
        // Combo interuption
        if (isDead || bKnockedDown)
            return;

        bDamaged = true;
        pawn.GetAnimator().Play("TakeDamage");
        currentHealth -= damageTaken;
        if (healthUI)
        {
            healthUI.SetDamaged(true);
            healthUI.SetSlider(maxHealth - currentHealth);
        }

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Die()
    {
        pawn.GetAnimator().Play("Die");
        isDead = true;
    }
}
