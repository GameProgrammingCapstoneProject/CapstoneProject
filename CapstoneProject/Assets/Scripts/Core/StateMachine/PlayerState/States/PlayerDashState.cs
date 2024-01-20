using System.Collections;
using System.Collections.Generic;
using Core.Components;
using Core.Entity;
using Core.PlayerInput;
using UnityEngine;

namespace Core.StateMachine
{
    public class PlayerDashState : PlayerState
    {
        private float _lastTimeDashed;
        private float _dashDuration = 0.4f;
        private float _dashSpeed = 10f;
        private float _busyTime = 1f;
        public PlayerDashState(Player inputPlayer, string inputAnimName) : base(inputPlayer, inputAnimName)
        {
        }
        

        public override void StateBegin()
        {
            base.StateBegin();
            if (Player.IsBusy()) return;
            StateTimer = _dashDuration;
            //player.stats.MakeInvincible(true);
            float dashDirection = (Player.rb.CurrentFacingDirection == RigidbodyComponent.FacingDirections.RIGHT) ? 1 : -1;
            if (PlayerInputReader.Instance.movementAxis != 0)
                dashDirection = (Mathf.Approximately(PlayerInputReader.Instance.movementAxis, 1)) ? 
                    1 : -1;
            Player.rb.SetVelocity(_dashSpeed * dashDirection, Player.rb.velocity.y);
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            Player.StartCoroutine(nameof(Player.BusyFor), _busyTime);
            if (StateTimer < 0)
                Player.States.stateMachine.ChangeState(Player.States.idleState);
            if (Player.rb.velocity.y < 0)
                Player.States.stateMachine.ChangeState(Player.States.airState);
        }

        public override void StateEnd()
        {
            base.StateEnd();
            //player.stats.MakeInvincible(false);
        }
    }
}

