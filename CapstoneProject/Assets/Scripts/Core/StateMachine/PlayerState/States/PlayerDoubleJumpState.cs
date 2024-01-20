using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.PlayerInput;
using Core.StateMachine;
using UnityEngine;

public class PlayerDoubleJumpState : PlayerState
{
    private float _moveSpeedWhileOnAir = 0.8f;
    public PlayerDoubleJumpState(Player inputPlayer, string inputAnimName) : base(inputPlayer, inputAnimName)
    {
    }
    public override void StateBegin()
    {
        base.StateBegin();
        Player.canDoubleJump = false;
        Player.rb.SetVelocity(Player.rb.velocity.x * 1.2f, Player.GetJumpForce() * 1.2f);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

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
