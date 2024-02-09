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

    private void Start()
    {
        DashAbility.AbilityStart(_player, this);
        playerAbilities = new List<PlayerAbility>
        {
            ShieldAbility,
            BowShootingAbility,
            HealthRegenAbility,
            ProjectileShootingAbility,
            LightningStrikeAbility
        };
        foreach (PlayerAbility ability in playerAbilities)
        {
            ability.AbilityStart(_player, this);
            ability.Unlock();
        }
    }

    private void Update()
    {
        DashAbility.AbilityUpdate();
        foreach (PlayerAbility ability in playerAbilities)
        {
            ability.AbilityUpdate();
        }
    }

    public void AddAbility(PlayerAbility ability, int slotNumber)
    {
        if (playerAbilities[slotNumber] != null)
            playerAbilities[slotNumber] = ability;
        else
        {
            playerAbilities.Insert(slotNumber, ability);
        }
    }
    public void RemoveAbility(PlayerAbility ability)
    {
        playerAbilities.Remove(ability);
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
