using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthComponent : AComponent {

    public HealthUI healthUI;

    public int maxHealth;
    public int currentHealth;

    public float knockedDownDuration;

    bool bDead;
    bool bKnockedDown;

    public bool IsDead() { return bDead; }
    public bool IsKnockedDown() { return bKnockedDown; }

    protected override void Start()
    {
        base.Start();

        if (healthUI)
            healthUI.healthSlider.maxValue = maxHealth;
    }

    public void TakeDamage(int damageTaken)
    {
        if (bDead || bKnockedDown)
            return;

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
