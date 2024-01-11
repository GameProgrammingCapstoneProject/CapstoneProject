using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.PlayerInput;
using Core.StateMachine;
using UnityEngine;

namespace Core.StateMachine
{
    public class PlayerRunState : PlayerState
    {
        private const float _minOnAirVelocity = -0.01f;

        public PlayerRunState(Player inputPlayer, string inputAnimName) : base(inputPlayer, inputAnimName)
        {
        }
    
        public override void StateUpdate()
        {
            base.StateUpdate();
            Player.rb.SetVelocity(PlayerInputReader.Instance.movementAxis * Player.GetMoveSpeed(), Player.rb.velocity.y);
            if (Mathf.Approximately(0, Player.rb.velocity.x))
            {
                Player.States.stateMachine.ChangeState(Player.States.idleState);
            }
            if (Player.rb.velocity.y < _minOnAirVelocity)
                Player.States.stateMachine.ChangeState(Player.States.airState);
        }
    }
}

