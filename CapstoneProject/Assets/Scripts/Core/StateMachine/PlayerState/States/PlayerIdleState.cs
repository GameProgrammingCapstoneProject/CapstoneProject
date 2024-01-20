using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.StateMachine;
using UnityEngine;

namespace Core.StateMachine
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(Player inputPlayer, string inputAnimName) : base(inputPlayer, inputAnimName)
        {
        }
        public override void StateBegin()
        {
            base.StateBegin();
            Player.canDoubleJump = true;
            Player.rb.ResetToZeroVelocity();
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            if (Player.rb.velocity.y < 0)
                Player.States.stateMachine.ChangeState(Player.States.airState);
        }

        public override void StateEnd()
        {
            base.StateEnd();
        }
    }
}

