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
    private Player _player;

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
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    private void Update()
    {

        //if (Input.GetKeyUp(KeyCode.B))
        //{
        //    Debug.Log("Heath: " + GetHealth());
        //    TakeDamage(5);
        //    Debug.Log("Heath: " + GetHealth());
        //}

        if (health <= 0)
        {
            _player.CoinComponent.CollectCoins(GetComponent<CoinComponent>().GetCoins());
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        UnityEngine.Object.FindObjectOfType<SoundManager>().Play("EnemyHurt");
        this.health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health < 0)
        {
            UnityEngine.Object.FindObjectOfType<SoundManager>().Play("EnemyDeath");
            health = 0;
        }
    }

    public void DoDamage(int damage, Character target)
    {
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
}
