using Core.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationComponent : MonoBehaviour
{
    [SerializeField]
    Enemy _enemy;

    [SerializeField]
    LayerMask playerLayer;

    private void TriggerMeleeAttack()
    {
        Collider2D[] hitObj = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        foreach (Collider2D obj in hitObj)
        {
            if (obj.GetComponent<Player>() != null)
            {
                Player player = obj.GetComponent<Player>();
                _enemy.GetComponent<EnemyHealthComponent>().DoDamage(1, player);
            }
        }
    }
}
