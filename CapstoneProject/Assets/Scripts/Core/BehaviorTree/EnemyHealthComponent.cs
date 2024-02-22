using Core.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthComponent : MonoBehaviour, IDamageable
{
    public int health;
    public int maxHealth = 20;
    HealthBar healthBar;

    //public static event EventHandler OnHealthChanged;

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
            TakeDamage(5);
            Debug.Log("Heath: " + GetHealth());
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health < 0)
        {
            health = 0;
        }
    }

    public void DoDamage(int damage, Character target)
    {
        //StartCoroutine(Attack(damage));
        if (target.GetComponent<PlayerHealthComponent>() != null)
        {
            PlayerHealthComponent playerHealthComponent = target.GetComponent<PlayerHealthComponent>();
            if(playerHealthComponent.GetComponent<IDamageable>() != null)
            {
                IDamageable playerDamageable = playerHealthComponent.GetComponent<IDamageable>();
                playerDamageable.TakeDamage(damage);
            }
        }
    }

    //IEnumerator Attack(int damage)
    //{
    //    yield return new WaitForSeconds(1.0f);
    //    Collider2D player = Physics2D.OverlapCircle(transform.position, 1.5f, playerLayer);
    //    player.GetComponent<IDamageable>().TakeDamage(damage);
    //}
}
