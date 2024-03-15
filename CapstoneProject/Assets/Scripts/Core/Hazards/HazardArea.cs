using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

public class HazardArea : MonoBehaviour
{
    [SerializeField]
    private float _damageInterval = 2.5f; // Damage interval in seconds
    [SerializeField]
    private int _damageAmount = 1; // Amount of damage to apply
    private PlayerHealthComponent _playerHealthComponent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerHealthComponent>())
        {
            _playerHealthComponent = other.GetComponent<PlayerHealthComponent>();
            StartCoroutine(nameof(ApplyDamage));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            // Stop applying damage
            StopCoroutine(nameof(ApplyDamage));
        }
    }

    private IEnumerator ApplyDamage()
    {
        if (_playerHealthComponent != null)
        {
            while (true)
            {
                _playerHealthComponent.TakeDamage(_damageAmount);
                yield return new WaitForSeconds(_damageInterval);
            }
        }
    }
}
