using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

namespace Core.StateMachine
{
    public class PlayerJumpState : PlayerAirState
    {
        public PlayerJumpState(Player inputPlayer, string inputAnimName) : base(inputPlayer, inputAnimName)
        {
        }
        public override void StateBegin()
        {
            base.StateBegin();
            Player.rb.SetVelocity(Player.rb.velocity.x, Player.GetJumpForce());
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

