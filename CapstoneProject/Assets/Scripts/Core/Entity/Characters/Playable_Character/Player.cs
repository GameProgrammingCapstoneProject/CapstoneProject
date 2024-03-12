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
        public KeyItemComponent KeyComponent;

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
           // LoadPlayer(); 
            canDoubleJump = true;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                SavePlayer();
                Debug.Log("Player saved");
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                LoadPlayer();
                Debug.Log("Player Loaded");
            }
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
            int coins = CoinComponent.GetCoins();
          //  int keys = KeyComponent.GetKeys();
            SaveSystem.SavePlayer(this, health, coins);
        }

        public void LoadPlayer()
        {
            PlayerSaveData data = SaveSystem.LoadPlayer();

            if (data != null)
            {
                Vector3 position;

                CoinComponent.ChangeCoins(data.playerCoins);
                //KeyComponent.ChangeKeys(data.playerKeys);
                HealthComponent.ChangeHealth(data.playerHealth);
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