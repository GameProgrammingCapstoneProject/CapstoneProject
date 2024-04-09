using System.Collections;
using System.Collections.Generic;
using Core.Components;
using Core.Entity;
using Core.PlayerInput;
using Core.StateMachine;
using UnityEngine;

namespace Core.StateMachine
{
    public class PlayerBowShootingState : PlayerState
    {
        private GameObject _target;
        public PlayerBowShootingState(Player inputPlayer, string inputAnimName) : base(inputPlayer, inputAnimName)
        {
        }
        public override void StateBegin()
        {
            base.StateBegin();
            Player.rb.ResetToZeroVelocity();
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            if (AnimationEndTrigger)
                Player.States.stateMachine.ChangeState(Player.States.idleState);
        }

        public override void StateEnd()
        {
            base.StateEnd();
        }
    }
}

