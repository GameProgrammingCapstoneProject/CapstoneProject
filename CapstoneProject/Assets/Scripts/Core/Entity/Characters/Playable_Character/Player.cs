using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.StateMachine;
using UnityEngine;

namespace Core.Entity
{
    public class Player : Character
    {
        [SerializeField]
        private float _jumpForce = 8f;
        private bool _isBusy = false;
        public EntityStateMachine<PlayerState> stateMachine { get; private set; }
        public PlayerIdleState idleState { get; private set; }
        public PlayerRunState runState { get; private set; }
        public PlayerAirState airState { get; private set; }
        public PlayerJumpState jumpState { get; private set; }
        public PlayerGroundAttackState groundAttackState { get; private set; }
        public PlayerAirAttackState airAttackState { get; private set; }
        public PlayerInteractState interactState { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            stateMachine = new EntityStateMachine<PlayerState>();
            idleState = new PlayerIdleState(this, "Idle");
            runState = new PlayerRunState(this, "Run");
            airState = new PlayerAirState(this, "Jump");
            jumpState = new PlayerJumpState(this, "Jump");
            groundAttackState = new PlayerGroundAttackState(this, "GroundAttack");
            airAttackState = new PlayerAirAttackState(this, "AirAttack");
            interactState = new PlayerInteractState(this, "Interact");
        }
        protected override void Start()
        {
            base.Start();
            stateMachine.Initialize(idleState);
        }
        
        protected override void Update()
        {
            base.Update();
            stateMachine.currentState.StateUpdate();
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
            SetJumpForce(_jumpForce);
        }

        public float GetJumpForce() => _jumpForce;
        public void SetJumpForce(float jumpForce) => _jumpForce = jumpForce;
        public void EndAnimationTrigger() => stateMachine.currentState.EndAnimationTrigger();
    }
}