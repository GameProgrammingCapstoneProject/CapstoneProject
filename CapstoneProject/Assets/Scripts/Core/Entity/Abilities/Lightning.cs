using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    private List<EnemyHealthComponent> _enemyList;

    private LightningStrikeAbility _ability;
    [SerializeField] private int _damage = 5;

    private void Start()
    {
        _enemyList = new List<EnemyHealthComponent>();
    }

    private void SelfDestroy()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyHealthComponent>())
        {
            _enemyList.Add(other.GetComponent<EnemyHealthComponent>());
        }
    }

    private void TriggerDamage()
    {
        foreach (var enemy in _enemyList)
        {
            enemy.TakeDamage(_damage);
        }
    }
}
