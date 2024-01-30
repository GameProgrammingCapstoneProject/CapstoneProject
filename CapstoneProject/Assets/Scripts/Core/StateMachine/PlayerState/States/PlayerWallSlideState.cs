using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.PlayerInput;
using Core.StateMachine;
using UnityEngine;

namespace Core.StateMachine
{
    public class PlayerWallSlideState : PlayerState
    {
        public PlayerWallSlideState(Player inputPlayer, string inputAnimName) : base(inputPlayer, inputAnimName)
        {
        }
        public override void StateBegin()
        {
            base.StateBegin();
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            /*if (Input.GetKeyDown(KeyCode.Space))
            {
                stateMachine.ChangeState(player.wallJumpState);
                return;
            }*/

            
            Player.rb.SetVelocity(0, Player.rb.velocity.y * 0.9f);

            if (Player.CollisionComponent.IsStandingOnGround())
                Player.States.stateMachine.ChangeState(Player.States.idleState);
        }

        public override void StateEnd()
        {
            base.StateEnd();
        }
    }
}

