using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.StateMachine;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player inputPlayer, string inputAnimName) : base(inputPlayer, inputAnimName)
    {
    }
}
