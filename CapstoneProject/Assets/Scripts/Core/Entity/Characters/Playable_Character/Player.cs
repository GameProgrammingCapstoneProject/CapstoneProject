using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.StateMachine;
using UnityEngine;

namespace Core.Entity
{
    public class Player : Entity
    {
        public EntityStateMachine<PlayerState> stateMachine { get; private set; }
        public PlayerIdleState idleState { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            stateMachine = new EntityStateMachine<PlayerState>();
            idleState = new PlayerIdleState(this, "Idle");
        }
        protected override void Start()
        {
            base.Start();
            stateMachine.Initialize(idleState);
        }
        
        void Update()
        {
            stateMachine.currentState.StateUpdate();
        }
    }
}