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
using TMPro;

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
        public SoulComponent SoulComponent;
        public KeyItemComponent KeyItemComponent;
        public TMP_Text SaveText;
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
            SaveText.enabled = false;

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
            bool[] unlockedAbilities = new bool [5];
            
            unlockedAbilities[0] = serializeLearnedAbility(AbilityComponent.HealthRegenAbility);
            unlockedAbilities[1] = serializeLearnedAbility(AbilityComponent.ShieldAbility);
            unlockedAbilities[2] = serializeLearnedAbility(AbilityComponent.ProjectileShootingAbility);
            unlockedAbilities[3] = serializeLearnedAbility(AbilityComponent.BowShootingAbility);
            unlockedAbilities[4] = serializeLearnedAbility(AbilityComponent.LightningStrikeAbility);



            SaveSystem.SavePlayer(this, health, coins, keys, abilityone, abilitytwo, sceneSaved, unlockedAbilities);
            StartCoroutine(showSaveText(3));
        }

        public IEnumerator showSaveText(float seconds)
        {
            SaveText.enabled = true;
          yield return new WaitForSeconds(seconds);
            SaveText.enabled = false;
        }
        public bool LoadPlayer()
        {

       
            PlayerSaveData data = SaveSystem.LoadPlayer();

            string scene = SceneManager.GetActiveScene().name;
            

                if (data != null)
                
                {
               
                   

                 //   Debug.Log("The scene the player was in was" + data.playerScene);
                    CoinComponent.ChangeCoins(data.playerCoins);
                    KeyItemComponent.ChangeKeys(data.playerKeys);
                    HealthComponent.ChangeHealth(data.playerHealth);

                    PlayerAbility abilityOne = deserializeAbility(data.playerAbilityOne);
                    PlayerAbility abilityTwo = deserializeAbility(data.playerAbilityTwo);



                    AbilityComponent.SetupPlayerAbility(abilityOne, 0);
                    AbilityComponent.SetupPlayerAbility(abilityTwo, 1);
                    deserializeLearnedAbility(data.playerUnlockedAbilities[0], AbilityComponent.HealthRegenAbility);
                    deserializeLearnedAbility(data.playerUnlockedAbilities[1], AbilityComponent.ShieldAbility);
                    deserializeLearnedAbility(data.playerUnlockedAbilities[2], AbilityComponent.ProjectileShootingAbility);
                    deserializeLearnedAbility(data.playerUnlockedAbilities[3], AbilityComponent.BowShootingAbility);
                    deserializeLearnedAbility(data.playerUnlockedAbilities[4], AbilityComponent.LightningStrikeAbility);
             
                //AbilityComponent.ChangeAbilties(data.playerAbilityOne, data.playerAbilityTwo);
                if (scene == data.playerScene)
                {
                //    Debug.Log(data.playerPosition[0]);
                    Vector3 position;
                    position.x = data.playerPosition[0];
                    position.y = data.playerPosition[1];
                    position.z = data.playerPosition[2];
                    transform.position = position;
                    return true;
                }
                else
                {
                    Debug.Log("Player Load partially completed but didn't load position due to scene not matching. If this happened after a level transistion, this is not a bug.");
                    return false;
                }
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
                case HealthRegenAbility:
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
                    abilityDeserialized = AbilityComponent.HealthRegenAbility;
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

        public bool serializeLearnedAbility(PlayerAbility ability)
        {
            if (ability._isUnlocked)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void deserializeLearnedAbility(bool abilityUnlocked, PlayerAbility ability)
        {
            if (abilityUnlocked) 
            {
            ability._isUnlocked = true;
            }
            else
            {
                ability._isUnlocked = false;
            }
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