using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth
{
    private int health;
    private int healthMax = 100;

    public static event EventHandler OnHealthChanged;
    public PlayerHealth(int healthMax)
    {
        this.health = healthMax;
        health = healthMax;
    }

    public int GetHealth()
    {
        return health;
    }

    public float GetHealthPercent()
    {
        return (float)health / healthMax;
    }

    public void TakeDamage(int damageTaken)
    {
        health -= damageTaken;   
        if (health < 0) { health = 0; }
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public void TakeHealing(int healthHealed)
    {
        health += healthHealed;
        if (health > healthMax) { health = healthMax; }
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }
}
