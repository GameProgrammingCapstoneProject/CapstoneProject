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

    public void Setup(float existTime, float moveSpeed)
    {
        _existTime = existTime;
        _moveSpeed = moveSpeed;
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
        }
    }
    private void HitCollision()
    {
        _fireBallCollider.enabled = false;
        _explosionCollider.enabled = true;
        _animator.SetTrigger("Explode");
        _explode = true;
    }

    private void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
