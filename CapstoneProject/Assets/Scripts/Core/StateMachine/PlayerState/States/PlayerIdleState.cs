using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.StateMachine;
using UnityEngine;

namespace Core.StateMachine
{
    public class PlayerIdleState : PlayerOnGroundState
    {
        public PlayerIdleState(Player inputPlayer, string inputAnimName) : base(inputPlayer, inputAnimName)
        {
        }
        public override void StateBegin()
        {
            base.StateBegin();
            Player.ResetToZeroVelocity();
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            if (HorizontalInput != 0)
                Player.stateMachine.ChangeState(Player.runState);
        }

        public override void StateEnd()
        {
            base.StateEnd();
        }
    }
}

