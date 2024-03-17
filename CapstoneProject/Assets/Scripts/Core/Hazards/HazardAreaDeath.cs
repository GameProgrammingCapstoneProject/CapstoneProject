using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

public class HazardAreaDeath : MonoBehaviour
{
    private PlayerHealthComponent _playerHealthComponent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerHealthComponent>())
        {
            _playerHealthComponent = other.GetComponent<PlayerHealthComponent>();
            _playerHealthComponent.KillPlayer();
        }
    }
}
