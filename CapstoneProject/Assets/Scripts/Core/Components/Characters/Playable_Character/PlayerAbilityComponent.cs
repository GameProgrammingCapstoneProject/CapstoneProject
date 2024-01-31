using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

public class PlayerAbilityComponent : MonoBehaviour
{
    public DashAbility DashAbility { get; private set; }
    public ShieldAbility ShieldAbility { get; private set; }
    public BowShootingAbility BowShootingAbility { get; private set; }

    [Header("Dash Ability Information")]
    [SerializeField]
    private float _dashCooldown;

    [Header("Shield Ability Information")]
    [SerializeField]
    private GameObject _shieldPrefab;
    [SerializeField]
    private float _shieldCooldown;
    [SerializeField]
    private float _shieldExistDuration;

    [Header("Bow Shooting Ability Information")]
    [SerializeField]
    private GameObject _arrowPrefab;
    [SerializeField]
    private float _bowShootingAbilityCooldown;
    [SerializeField]
    private Transform _bowShootingPosition;
    [SerializeField]
    private float _bowAbilityExistDuration;
    
    
    private Player _player;
    public List<PlayerAbility> playerAbilities;
    private void Awake()
    {
        _player = GetComponent<Player>();
        DashAbility = new DashAbility(_player, _dashCooldown);
        ShieldAbility = new ShieldAbility(_player, _shieldCooldown, _shieldPrefab, _shieldExistDuration);
        BowShootingAbility = new BowShootingAbility(_player, _bowShootingAbilityCooldown, _arrowPrefab, _bowShootingPosition, _bowAbilityExistDuration);
    }

    private void Start()
    {
        playerAbilities = new List<PlayerAbility>
        {
            ShieldAbility,
            BowShootingAbility
        };
    }

    private void Update()
    {
        DashAbility.Update();
        foreach (PlayerAbility ability in playerAbilities)
        {
            ability.Update();
        }
    }
}
