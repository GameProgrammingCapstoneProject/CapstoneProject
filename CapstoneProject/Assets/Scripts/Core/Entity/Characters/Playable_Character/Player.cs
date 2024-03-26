using System;
using System.Collections;
using System.Collections.Generic;
using Core.Components;
using Core.Entity;
using Core.PlayerInput;
using Core.StateMachine;
using UnityEngine;
using System.IO;
using Core.GameStates;
using UnityEngine.SceneManagement;

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
        [HideInInspector]
        public bool isSteppingOnElevator = false;

        private Transform _elevator;
        private Scene scene;
        protected override void Start()
        {

            Debug.Log(Application.persistentDataPath);
            base.Start();

            if (LoadPlayer() == false)
            {
                SavePlayer();
            }


            canDoubleJump = true;
        }

        /*public void Update()
        {
          if (Input.GetKeyDown(KeyCode.J))
            {
                //SavePlayer();
                DeleteSave();
                Debug.Log("Player DELETED!!!!!!!!!!");
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                LoadPlayer();
                Debug.Log("Player Loaded");
            }
        }*/

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
            scene = SceneManager.GetActiveScene();
            Debug.Log(" Scene Name: " + scene.name);
            string sceneSaved = scene.name;
            int health = HealthComponent.GetHealth();
            int keys = KeyItemComponent.GetKeys();
            int coins = CoinComponent.GetCoins();



            List<PlayerAbility> ability = AbilityComponent.GetAbilities();
            int abilityone = serializeAbility(ability[0]);
            int abilitytwo = serializeAbility(ability[1]);

            
            SaveSystem.SavePlayer(this, health, coins, keys, abilityone, abilitytwo, sceneSaved);

        }


        public bool LoadPlayer()
        {

            PlayerSaveData data = SaveSystem.LoadPlayer();

            if (data != null)
            {
                Vector3 position;

                Debug.Log("The scene the player was in was" + data.playerScene);
                CoinComponent.ChangeCoins(data.playerCoins);
                KeyItemComponent.ChangeKeys(data.playerKeys);
                HealthComponent.ChangeHealth(data.playerHealth);

                PlayerAbility abilityOne = deserializeAbility(data.playerAbilityOne);
                PlayerAbility abilityTwo = deserializeAbility(data.playerAbilityTwo);
                AbilityComponent.SetupPlayerAbility(abilityOne, 0);
                AbilityComponent.SetupPlayerAbility(abilityTwo, 1);
                //AbilityComponent.ChangeAbilties(data.playerAbilityOne, data.playerAbilityTwo);
                Debug.Log(data.playerPosition[0]);

                position.x = data.playerPosition[0];
                position.y = data.playerPosition[1];
                position.z = data.playerPosition[2];
                transform.position = position;
                return true;

            }
            else
            {
                Debug.Log("Player Load attempted but no save data was found.");
                return false;
            }

        }

        public int serializeAbility(PlayerAbility ability)
        {
            int abilitySerialized;
            switch (ability)
            {
                case DashAbility:
                    abilitySerialized = 1;
                    break;
                case ShieldAbility:
                    abilitySerialized = 2;
                    break;
                case BowShootingAbility:
                    abilitySerialized = 3;
                    break;
                case ProjectileShootingAbility:
                    abilitySerialized = 4;
                    break;
                case LightningStrikeAbility:
                    abilitySerialized = 5;
                    break;
                case null:
                    abilitySerialized = 0;
                    break;
                default:
                    // Debug.Log(ability[0]);
                    Debug.Log("Ability one not found");
                    abilitySerialized = 0;
                    break;
            }
            return abilitySerialized;

        }
        public PlayerAbility deserializeAbility(int abilityData)
        {
            PlayerAbility abilityDeserialized;
            switch (abilityData)
            {
                case 1:
                    abilityDeserialized = AbilityComponent.DashAbility;
                    break;
                case 2:
                    abilityDeserialized = AbilityComponent.ShieldAbility;
                    break;
                case 3:
                    abilityDeserialized = AbilityComponent.BowShootingAbility;
                    break;
                case 4:
                    abilityDeserialized = AbilityComponent.ProjectileShootingAbility;
                    break;
                case 5:
                    abilityDeserialized = AbilityComponent.LightningStrikeAbility;
                    break;
                case 0:
                    abilityDeserialized = null;
                    break;
                default:
                    // Debug.Log(ability[0]);
                    Debug.Log("Ability one not found");
                    abilityDeserialized = null;
                    break;
            }
            return abilityDeserialized;
        }
        private string SavePath
        {
    
            get { return Application.persistentDataPath + "/player.STH"; }
        }
        public void DeleteSave()
        {
            try
            {
                File.Delete(SavePath);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Elevator>())
            {
                isSteppingOnElevator = true;
                _elevator = other.transform;
                transform.parent = _elevator.transform;
                GameState.Instance.CurrentGameState = GameState.States.CutScene;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<Elevator>())
            {
                isSteppingOnElevator = false;
                _elevator = null;
                transform.parent = null;
            }
        }
    }
}