using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
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
        if (ability == null)
        {
            playerAbilities[slotNumber] = null;
            OnCurrentAbilitiesChanged?.Invoke(null, -1);
        }
        else
        {
            playerAbilities[slotNumber] = ability;
            ability.AbilityStart(_player, this);
            Debug.Log(_player);
            Debug.Log(this);
            OnCurrentAbilitiesChanged?.Invoke(ability, slotNumber);
        }
        
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

    public List<PlayerAbility> GetAbilities() => playerAbilities;

    public void ChangeAbilties(int abilityone, int abilitytwo)
    {
        List<PlayerAbility> ability = new List<PlayerAbility>();
        switch (abilityone)
        {
            case 1:
                ability[0] = DashAbility;
                break;
            case 2:
                ability[0] = ShieldAbility;
                break;
            case 3:
                ability[0] = BowShootingAbility;
                break;
            case 4:
                ability[0] = ProjectileShootingAbility;
                break;
            case 5 :
                ability[0] = LightningStrikeAbility;
                break;
            case 0:
               ability[0] = null;
                break;
            default:
                Debug.Log(ability[0]);
                Debug.Log("Ability one not found");
                ability[0] = null;
                break;
        }
        switch (abilitytwo)
        {
            case 1:
                ability[1] = DashAbility;
                break;
            case 2:
                ability[1] = ShieldAbility;
                break;
            case 3:
                ability[1] = BowShootingAbility;
                break;
            case 4:
                ability[1] = ProjectileShootingAbility;
                break;
            case 5:
                ability[1] = LightningStrikeAbility;
                break;
            case 0:
                ability[1] = null;
                break;
            default:
                Debug.Log(ability[1]);
                Debug.Log("Ability two not found");
                ability[1] = null ;
                break;
        }
        SetupPlayerAbility(ability[0], 0);
        SetupPlayerAbility(ability[1], 1);
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (DashAbility == null || ShieldAbility == null || BowShootingAbility == null ||
            HealthRegenAbility == null || ProjectileShootingAbility == null || LightningStrikeAbility == null)
        {
            Debug.LogWarning("One or more Player Abilities are not assigned in the inspector!");
        }
    }
#endif
}
