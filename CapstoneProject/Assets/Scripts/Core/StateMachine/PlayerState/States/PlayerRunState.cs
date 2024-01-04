using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.StateMachine;
using UnityEngine;

public class PlayerRunState : PlayerOnGroundState
{
    public PlayerRunState(Player inputPlayer, string inputAnimName) : base(inputPlayer, inputAnimName)
    {
    }
    
    public override void StateUpdate()
    {
        base.StateUpdate();
        Player.SetVelocity(HorizontalInput * Player.GetMoveSpeed(), Rb.velocity.y);
        if (Mathf.Approximately(0, Rb.velocity.x))
        {
            Player.stateMachine.ChangeState(Player.idleState);
        }
    }
}
