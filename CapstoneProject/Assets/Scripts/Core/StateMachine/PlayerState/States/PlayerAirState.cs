using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.PlayerInput;
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

            // For testing
            /*if (Input.GetKey(KeyCode.A))
                Player.States.stateMachine.ChangeState(Player.States.airAttackState);*/

            if (Player.CollisionComponent.IsInteractingWithWall() && PlayerInputReader.Instance.movementAxis != 0)
                Player.States.stateMachine.ChangeState(Player.States.wallSlideState);
            
            
            if (Mathf.Approximately(0, Player.rb.velocity.y))
            {
                Player.States.stateMachine.ChangeState(Player.States.idleState);
            }

      
            if (PlayerInputReader.Instance.movementAxis != 0)
                Player.rb.SetVelocity(Player.GetMoveSpeed() * _moveSpeedWhileOnAir * PlayerInputReader.Instance.movementAxis, Player.rb.velocity.y);
        }

        public override void StateEnd()
        {
            base.StateEnd();
        }
    }
}

