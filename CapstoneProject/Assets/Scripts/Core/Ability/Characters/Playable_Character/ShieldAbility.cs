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
    private Player _player;
    public event System.Action OnShieldAbilityCoolDown;
    protected override void Activate()
    {
        if (_currentShield == null)
        {
            _player = GetComponent<Player>();
            _currentShield = Instantiate(_shieldPrefab, _player.transform.position, _player.transform.rotation);
            Shield shield = _currentShield.GetComponent<Shield>();
            shield.Setup(_existDuration, _player);
            OnShieldAbilityCoolDown?.Invoke();
        }
    }
}
