using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

public class PlayerHealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField]
    private CharacterEffect _effect;
    public int currentHealth { get; private set; }
    public bool isDead { get; private set; }
    public bool isInvincible { get; private set; }
    public event System.Action OnHealthChanged;
    [SerializeField]
    private int _maxHealth = 6;

    private void Start()
    {
        currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead || isInvincible) return;
        DecreaseHealthBy(damage);
        _effect.ShakeScreen();
        _effect.StartCoroutine(nameof(_effect.FlashFX));
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void DoDamage(int damage, Character target)
    {
        if (target.GetComponent<EnemyHealthComponent>())
        {
            EnemyHealthComponent enemyHealthComponent = target.GetComponent<EnemyHealthComponent>();
            if (enemyHealthComponent.GetComponent<IDamageable>() != null)
            {
                IDamageable enemyDamageable = enemyHealthComponent.GetComponent<IDamageable>();
                enemyDamageable.TakeDamage(damage);
                _effect.GenerateHitFX(target.transform);
            }
            else
            {
                Debug.Assert(true, "Target has no IDamageable interface");
            }
        }
        else
        {
            Debug.Assert(true, "Target has no health component");
        }
    }

    private void Die()
    {
        isDead = true;
    }
    
    public void KillPlayer()
    {
        if (!isDead)
            Die();
    }
    
    public virtual void DecreaseHealthBy(int inputDamage)
    {
        currentHealth -= inputDamage;
        OnHealthChanged?.Invoke();
    }
    public void MakeInvincible(bool inputIsInvincible) => isInvincible = inputIsInvincible;
    
    public virtual void IncreaseHealthBy(int inputHealth)
    {
        currentHealth += inputHealth;
        if (currentHealth > GetMaxHealthValue())
            currentHealth = GetMaxHealthValue();
        OnHealthChanged?.Invoke();
    }

    public int GetMaxHealthValue() => _maxHealth;
}
