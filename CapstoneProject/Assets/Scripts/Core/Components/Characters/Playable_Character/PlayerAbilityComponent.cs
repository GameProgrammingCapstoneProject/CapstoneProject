using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

public class PlayerAbilityComponent : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    public DashAbility DashAbility;
    public ShieldAbility ShieldAbility;
    public BowShootingAbility BowShootingAbility;
    public HealthRegenAbility HealthRegenAbility;
    public ProjectileShootingAbility ProjectileShootingAbility;
    public LightningStrikeAbility LightningStrikeAbility;
    
    private Coroutine _coroutine;
    [SerializeField]
    private float _scanRadius = 6f;
    public List<PlayerAbility> playerAbilities { get; private set; }
    public GameObject lowestHealthTarget { get; private set; }
    [SerializeField] private LayerMask _enemyLayerMask;
    // Define a delegate for the event
    public delegate void PlayerAbilityChangedHandler(PlayerAbility ability, int index);
    
    // Define the event using the delegate
    public static event PlayerAbilityChangedHandler OnCurrentAbilitiesChanged;

    private void Start()
    {
        DashAbility.AbilityStart(_player, this);
        playerAbilities = new List<PlayerAbility>
        {
            null,
            null
        };
    }

    private void Update()
    {
        DashAbility.AbilityUpdate();
        foreach (PlayerAbility ability in playerAbilities)
        {
            if (ability != null)
                ability.AbilityUpdate();
        }
    }

    public void SetupPlayerAbility(PlayerAbility ability, int slotNumber)
    {
        if (playerAbilities[slotNumber] == null)
            playerAbilities[slotNumber] = ability;
        else
        {
            playerAbilities[slotNumber] = ability;
        }
        ability.AbilityStart(_player, this);
        OnCurrentAbilitiesChanged?.Invoke(ability, slotNumber);
    }
    public void StartRoutine(PlayerAbility ability)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ability.ActivateRoutine());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _scanRadius);
    }

    public GameObject[] ScanForEnemiesInArea()
    {
        Collider2D[] allEnemiesCollider = Physics2D.OverlapCircleAll(this.transform.position, _scanRadius, _enemyLayerMask);
        List<GameObject> allEnemies = new List<GameObject>();
        foreach (Collider2D enemyCollider in allEnemiesCollider)
        {
            allEnemies.Add(enemyCollider.gameObject);
        }
        return allEnemies.ToArray();
    }
    public GameObject ScanForLowestHealthEnemy()
    {
        //TODO: Need to implement the this function
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject lowestHealthEnemy = targets[0];
        lowestHealthTarget = lowestHealthEnemy;
        
        return lowestHealthTarget;
    }
}
