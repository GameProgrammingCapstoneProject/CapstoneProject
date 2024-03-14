using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private BoxCollider2D _fireBallCollider;
    [SerializeField]
    private CircleCollider2D _explosionCollider;
    private float _existTime;
    private float _moveSpeed;
    private bool _explode = false;
    private List<EnemyHealthComponent> _enemyList;

    public void Setup(float existTime, float moveSpeed)
    {
        _existTime = existTime;
        _moveSpeed = moveSpeed;
        SoundManager.Instance.Play("FireballCast");
    }

    private void Start()
    {
        _enemyList = new List<EnemyHealthComponent>();
    }

    private void Update()
    {
        if (_explode) return;
        _existTime -= Time.deltaTime;
        if (_existTime < 0)
            SelfDestroy();
        transform.position += (Vector3)(transform.right * (_moveSpeed * Time.deltaTime));
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Ground") || 
            col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            HitCollision();
            if (col.GetComponent<EnemyHealthComponent>())
            {
                if (!_enemyList.Contains(col.GetComponent<EnemyHealthComponent>()))
                    _enemyList.Add(col.GetComponent<EnemyHealthComponent>());
            }
        }
    }
    private void HitCollision()
    {
        SoundManager.Instance.Play("FireballExplode");
        _fireBallCollider.enabled = false;
        _explosionCollider.enabled = true;
        _animator.SetTrigger("Explode");
        _explode = true;
    }

    private void TriggerDamage()
    {
        if (_enemyList.Count > 0)
        {
            foreach (var enemy in _enemyList)
            {
                enemy.TakeDamage(30);
            }
        }
        
    }

    private void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
