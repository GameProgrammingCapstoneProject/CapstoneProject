using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.StateMachine;
using UnityEngine;

namespace Core.StateMachine
{
    public class PlayerBowShootingState : PlayerState
    {
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
