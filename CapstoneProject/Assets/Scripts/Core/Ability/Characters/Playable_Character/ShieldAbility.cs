using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

public class ShieldAbility : PlayerAbility
{
    private GameObject _shieldPrefab;
    private GameObject _currentShield;
    private float _existDuration;
    public event System.Action OnShieldAbilityCoolDown;
    public ShieldAbility(Player player, float cooldown, GameObject shieldPrefab, float existDuration) : base(player, cooldown)
    {
        _shieldPrefab = shieldPrefab;
        _existDuration = existDuration;
    }
    protected override void Activate()
    {
        if (_currentShield == null)
        {
            _currentShield = GameObject.Instantiate(_shieldPrefab, Instigator.transform.position, Instigator.transform.rotation);
            Shield shield = _currentShield.GetComponent<Shield>();
            shield.Setup(_existDuration, Instigator);
            OnShieldAbilityCoolDown?.Invoke();
        }
    }
}
