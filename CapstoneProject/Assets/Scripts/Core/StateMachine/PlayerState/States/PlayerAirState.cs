using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

namespace Core.StateMachine
{
    public class PlayerAirState : PlayerState
    {
        private float _moveSpeedWhileOnAir = 0.8f;
        public PlayerAirState(Player inputPlayer, string inputAnimName) : base(inputPlayer, inputAnimName)
        {
        }
        public override void StateBegin()
        {
            base.StateBegin();
        }

        public override void StateUpdate()
        {
            base.StateUpdate();

            if (Mathf.Approximately(0, Player.rb.velocity.y))
            {
                Player.stateMachine.ChangeState(Player.idleState);
            }

            if (HorizontalInput != 0)
                Player.SetVelocity(Player.GetMoveSpeed() * _moveSpeedWhileOnAir * HorizontalInput, Player.rb.velocity.y);
        }

        public override void StateEnd()
        {
            base.StateEnd();
        }
    }
}

