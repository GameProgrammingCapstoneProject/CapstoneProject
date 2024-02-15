using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public int health;
    public int maxHealth = 20;
    HealthBar healthBar;

    public static event EventHandler OnHealthChanged;
  /*  public EnemyHealth(int healthMax)
    {
        this.health = healthMax;
        health = healthMax;
    }*/


    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }
    public int GetHealth()
    {
        return health;
    }

    public float GetHealthPercent()
    {
        //return (float)health / healthMax;

        return 0.5f;
    }

    /* public void TakeDamage(int damageTaken)
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
     }*/

    public void Damage(int damage)
    {
        this.health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health < 0) { health = 0; }

    }

    private void Start()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.B))
        {
            Debug.Log("Heath: " + GetHealth());
            Damage(5);
            Debug.Log("Heath: " + GetHealth());
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
