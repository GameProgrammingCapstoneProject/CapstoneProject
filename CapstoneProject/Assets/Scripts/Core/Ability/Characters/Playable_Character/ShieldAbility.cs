using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

public class ShieldAbility : PlayerAbility
{
    [SerializeField]
    private GameObject _shieldPrefab;
    private GameObject _currentShield;
    [SerializeField]
    private float _existDuration = 5f;
    public event System.Action OnShieldAbilityCoolDown;
    protected override void Activate()
    {
        if (_currentShield == null)
        {
            _currentShield = Instantiate(_shieldPrefab, GetComponent<Player>().transform.position, GetComponent<Player>().transform.rotation);
            Shield shield = _currentShield.GetComponent<Shield>();
            shield.Setup(_existDuration, GetComponent<Player>());
            OnShieldAbilityCoolDown?.Invoke();
        }
    }
}
