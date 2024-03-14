using System.Collections;
using System.Collections.Generic;
using Core.Components;
using Core.Entity;
using Core.PlayerInput;
using Core.StateMachine;
using UnityEngine;

namespace Core.Entity
{
    [RequireComponent(typeof(PlayerStateComponent))]
    [RequireComponent(typeof(CollisionComponent))]
    [RequireComponent(typeof(PlayerAbilityComponent))]
    [RequireComponent(typeof(CoinComponent))]
    [RequireComponent(typeof(KeyItemComponent))]
    public class Player : Character
    {
        public PlayerStateComponent States;
        public CollisionComponent CollisionComponent;
        public PlayerAbilityComponent AbilityComponent;
        public PlayerHealthComponent HealthComponent;
        public CoinComponent CoinComponent;
        public KeyItemComponent KeyItemComponent;

        public Transform bowShootingPosition;
        public Transform projectileShootingPosition;
        //TODO: The movement attributes needs to be made into one separate component
        [Header("Movement information")]
        [SerializeField]
        protected float _moveSpeed = 5f;
        [SerializeField]
        private float _jumpForce = 8f;
        [HideInInspector]
        public bool canDoubleJump = false;
        private bool _isBusy = false;
        protected override void Start()
        {
            base.Start();
            LoadPlayer(); 
            canDoubleJump = true;
        }

        public void Update()
        {
/*            if (Input.GetKeyDown(KeyCode.J))
            {
                SavePlayer();
                Debug.Log("Player saved");
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                LoadPlayer();
                Debug.Log("Player Loaded");
            }*/
        }

        public bool IsBusy() => _isBusy;
        public IEnumerator BusyFor(float seconds)
        {
            _isBusy = true;
            yield return new WaitForSeconds(seconds);
            _isBusy = false;
        }

        protected override void InitialSetup()
        {
            base.InitialSetup();
            SetMoveSpeed(_moveSpeed);
            SetJumpForce(_jumpForce);
        }

        public float GetJumpForce() => _jumpForce;
        public void SetJumpForce(float jumpForce) => _jumpForce = jumpForce;
        public float GetMoveSpeed() => _moveSpeed;
        public float SetMoveSpeed(float moveSpeed) => _moveSpeed = moveSpeed;
        public void EndAnimationTrigger() => States.stateMachine.currentState.EndAnimationTrigger();

        public void SavePlayer()
        {
            int health = HealthComponent.currentHealth;
            int keys = KeyItemComponent.GetKeys();
            int coins = CoinComponent.GetCoins();



            List<PlayerAbility> ability = AbilityComponent.GetAbilities();
            int abilityone;
            int abilitytwo;
            switch (ability[0])
            {
                case DashAbility:
                    abilityone = 1;
                    break;
                case ShieldAbility:
                    abilityone = 2;
                    break;
                case BowShootingAbility:
                    abilityone = 3;
                    break;
                case ProjectileShootingAbility:
                    abilityone = 4;
                    break;
                case LightningStrikeAbility:
                    abilityone = 5;
                    break;
                case null:
                    abilityone = 0;
                    break;
                default:
                    Debug.Log(ability[0]);
                    Debug.Log("Ability one not found");
                    abilityone = 0;
                    break;
            }
            switch (ability[1])
            {
                case DashAbility:
                    abilitytwo = 1;
                    break;
                case ShieldAbility:
                    abilitytwo = 2;
                    break;
                case BowShootingAbility:
                    abilitytwo = 3;
                    break;
                case ProjectileShootingAbility:
                    abilitytwo = 4;
                    break;
                case LightningStrikeAbility:
                    abilitytwo = 5;
                    break;
                case null:
                    abilitytwo = 0;
                    break;
                default:
                    Debug.Log(ability[1]);
                    Debug.Log("Ability two not found");
                    abilitytwo = 0;
                    break;
            }
            SaveSystem.SavePlayer(this, health, coins, keys, abilityone, abilitytwo);
        }

        public void LoadPlayer()
        {
            PlayerSaveData data = SaveSystem.LoadPlayer();

            if (data != null)
            {
                Vector3 position;

                CoinComponent.ChangeCoins(data.playerCoins);
                KeyItemComponent.ChangeKeys(data.playerKeys);
                HealthComponent.ChangeHealth(data.playerHealth);
                var ability = AbilityComponent.playerAbilities;
/*                List<PlayerAbility> ability = new List<PlayerAbility>()
                {
                    null,
                    null
                };*/
                switch (data.playerAbilityOne)
                {
                    case 1:
                        ability[0] = AbilityComponent.DashAbility;
                        break;
                    case 2:
                        ability[0] = AbilityComponent.ShieldAbility;
                        break;
                    case 3:
                        ability[0] = AbilityComponent.BowShootingAbility;
                        break;
                    case 4:
                        ability[0] = AbilityComponent.ProjectileShootingAbility;
                        break;
                    case 5:
                        ability[0] = AbilityComponent.LightningStrikeAbility;
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
                switch (data.playerAbilityTwo)
                {
                    case 1:
                        ability[1] = AbilityComponent.DashAbility;
                        break;
                    case 2:
                        ability[1] = AbilityComponent.ShieldAbility;
                        break;
                    case 3:
                        ability[1] = AbilityComponent.BowShootingAbility;
                        break;
                    case 4:
                        ability[1] = AbilityComponent.ProjectileShootingAbility;
                        break;
                    case 5:
                        ability[1] = AbilityComponent.LightningStrikeAbility;
                        break;
                    case 0:
                        ability[1] = null;
                        break;
                    default:
                        Debug.Log(ability[1]);
                        Debug.Log("Ability two not found");
                        ability[1] = null;
                        break;
                }
                
                AbilityComponent.SetupPlayerAbility(ability[0], 0);
                AbilityComponent.SetupPlayerAbility(ability[1], 1);
                //AbilityComponent.ChangeAbilties(data.playerAbilityOne, data.playerAbilityTwo);
                Debug.Log(data.playerPosition[0]);

                position.x = data.playerPosition[0];
                position.y = data.playerPosition[1];
                position.z = data.playerPosition[2];
                transform.position = position;


            }
            else
            {
                Debug.Log("Player Load attempted but no save data was found.");
            }

        }



        }
}