using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Shield Ability", fileName = "ShieldAbility", order = 1)]
public class ShieldAbility : PlayerAbility
{
    [SerializeField]
    private Shield _shieldPrefab;
    private Shield _currentShield;
    [SerializeField]
    private float _existDuration;
    public event System.Action<ShieldAbility> OnShieldAbilityCoolDown;
    protected override void Activate()
    {
        if (_currentShield == null)
        {
            _currentShield = GameObject.Instantiate(_shieldPrefab, Instigator.transform.position, Instigator.transform.rotation);
            _currentShield.Setup(_existDuration, Instigator);
            OnShieldAbilityCoolDown?.Invoke(this);
        }
    }
}
