using Core.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour, IDamageable
{
    private GameObject player;
    private Rigidbody2D rb;
    float force = 10;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        if(direction.x > 0)
        {
            rb.AddForce(transform.right * force, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(-transform.right * force, ForceMode2D.Impulse);
        }
        Destroy(this.gameObject, 4.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && collision.GetComponent<Player>() != null)
        {
            Player player = collision.GetComponent<Player>();
            DoDamage(1 , player);
        }
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    public void DoDamage(int damage, Character target)
    {
        if (target.GetComponent<PlayerHealthComponent>() != null)
        {
            PlayerHealthComponent playerHealthComponent = target.GetComponent<PlayerHealthComponent>();
            if (playerHealthComponent.GetComponent<IDamageable>() != null)
            {
                IDamageable playerDamageable = playerHealthComponent.GetComponent<IDamageable>();
                playerDamageable.TakeDamage(damage);
            }
        }
    }
}
