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
            if (Input.GetKeyDown(KeyCode.Space))
                Player.stateMachine.ChangeState(Player.jumpState);
            if (Input.GetKeyDown(KeyCode.A))
                Player.stateMachine.ChangeState(Player.groundAttackState);
            if (Input.GetKeyDown(KeyCode.E))
                Player.stateMachine.ChangeState(Player.interactState);
        }

        public override void StateEnd()
        {
            base.StateEnd();
        }
    }
}

