using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.StateMachine;
using UnityEngine;

namespace Core.StateMachine
{
    public class PlayerRunState : PlayerOnGroundState
    {
        public PlayerRunState(Player inputPlayer, string inputAnimName) : base(inputPlayer, inputAnimName)
        {
        }
    
        public override void StateUpdate()
        {
            base.StateUpdate();
            Player.SetVelocity(HorizontalInput * Player.GetMoveSpeed(), Player.rb.velocity.y);
            if (Mathf.Approximately(0, Player.rb.velocity.x))
            {
                Player.stateMachine.ChangeState(Player.idleState);
            }
            if (Player.rb.velocity.y < -0.01)
                Player.stateMachine.ChangeState(Player.airState);
        }
    }
}

