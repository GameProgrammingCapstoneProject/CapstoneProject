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
            _target = Player.AbilityComponent.ScanForLowestHealthEnemy();
            float attackDirection = 1;
            if (_target.transform.position.x > Player.transform.position.x)
            {
                attackDirection = 1;
            }
            else
            {
                attackDirection = -1;
            }
            Player.rb.SetVelocity(attackDirection, 0);
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

