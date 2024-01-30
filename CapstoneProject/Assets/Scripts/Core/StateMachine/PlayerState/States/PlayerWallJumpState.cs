using System.Collections;
using System.Collections.Generic;
using Core.Components;
using Core.Entity;
using Core.PlayerInput;
using Core.StateMachine;
using UnityEngine;

namespace Core.StateMachine
{
    public class PlayerWallJumpState : PlayerState
    {
        
        public PlayerWallJumpState(Player inputPlayer, string inputAnimName) : base(inputPlayer, inputAnimName)
        {
        }
        public override void StateBegin()
        {
            base.StateBegin();
            StateTimer = 0.4f;
            float jumpDirection = (Player.rb.CurrentFacingDirection == RigidbodyComponent.FacingDirections.RIGHT) ? 1 : -1;
            Player.rb.SetVelocity(Player.GetMoveSpeed() * -jumpDirection, Player.GetJumpForce());
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            if (StateTimer < 0)
                Player.States.stateMachine.ChangeState(Player.States.airState);
        
            if (Player.CollisionComponent.IsStandingOnGround() && Mathf.Approximately(Player.rb.velocity.y, 0))
                Player.States.stateMachine.ChangeState(Player.States.idleState);
        }

        public override void StateEnd()
        {
            base.StateEnd();
        }
    }
}

