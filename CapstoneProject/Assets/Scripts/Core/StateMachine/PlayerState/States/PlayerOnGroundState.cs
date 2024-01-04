using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

namespace Core.StateMachine
{
    public class PlayerOnGroundState : PlayerState
    {
        public PlayerOnGroundState(Player inputPlayer, string inputAnimName) : base(inputPlayer, inputAnimName)
        {
        }
        public override void StateBegin()
        {
            base.StateBegin();
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
        }

        public override void StateEnd()
        {
            base.StateEnd();
        }
    }
}

