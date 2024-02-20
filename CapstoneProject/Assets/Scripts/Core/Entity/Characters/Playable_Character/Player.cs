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
    public class Player : Character
    {
        public PlayerStateComponent States;
        public CollisionComponent CollisionComponent;
        public PlayerAbilityComponent AbilityComponent;

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
            canDoubleJump = true;
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
    }
}