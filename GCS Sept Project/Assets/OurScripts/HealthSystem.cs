using System;
using UnityEngine;

public class HealthSystem
{
    private int currentHealth;
    private int healthMax;
    public event EventHandler OnDeath;


    public HealthSystem(int healthMax)
    {
        this.healthMax = healthMax;
        currentHealth = healthMax;
    }


    public void Damage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Heal(int healing)
    {
        currentHealth += healing;
        if (currentHealth > healthMax)
        {
            currentHealth = healthMax;
        }
    }
}
