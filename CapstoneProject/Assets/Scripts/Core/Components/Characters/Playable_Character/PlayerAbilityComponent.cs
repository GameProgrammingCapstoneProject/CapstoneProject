using System;
using System.Collections;
using System.Collections.Generic;
using Core.Components;
using Core.Entity;
using UnityEngine;
//using UnityEditor.Playables;
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
    [SerializeField] private LayerMask _enemyLayerMask;
    public float viewAngle = 90f;
    public float viewRadius = 10f;
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
        Gizmos.color = Color.red;
        Vector2 direction = Quaternion.Euler(0, 0, viewAngle) * transform.right;
        Gizmos.DrawRay(transform.position, direction * viewRadius);
        direction = Quaternion.Euler(0, 0, -viewAngle) * transform.right;
        Gizmos.DrawRay(transform.position, direction * viewRadius);
        Gizmos.DrawWireSphere(transform.position, viewRadius);
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
    public Enemy ScanForNearestTarget()
    {
        Collider2D[] allEnemiesCollider = Physics2D.OverlapCircleAll(this.transform.position, viewRadius, _enemyLayerMask);

        List<Enemy> enemies = new List<Enemy>();
        Enemy nearestEnemy = null;
        if (allEnemiesCollider.Length > 0)
        {
            if (_player.rb.CurrentFacingDirection == RigidbodyComponent.FacingDirections.RIGHT)
            {
                foreach (var enemyInRange in allEnemiesCollider)
                {
                    if (enemyInRange.transform.position.x > _player.transform.position.x)
                    {
                        enemies.Add(enemyInRange.GetComponent<Enemy>());
                    }
                }
            }
            else
            {
                foreach (var enemyInRange in allEnemiesCollider)
                {
                    if (enemyInRange.transform.position.x < _player.transform.position.x)
                    {
                        enemies.Add(enemyInRange.GetComponent<Enemy>());
                    }
                }
            }
        }

        float distance = 0;
        if (enemies.Count > 0)
        {
            if (enemies.Count == 1)
            {
                nearestEnemy = enemies[0];
            }
            else
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (i == 0)
                        distance = Vector3.Distance(enemies[0].transform.position, _player.transform.position);
                    else
                    {
                        float newDistance = Vector3.Distance(enemies[i].transform.position, _player.transform.position);
                        if (newDistance < distance)
                        {
                            distance = newDistance;
                            nearestEnemy = enemies[i].GetComponent<Enemy>();
                        }
                    }
                }
            }

        }
        
        return nearestEnemy;
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
