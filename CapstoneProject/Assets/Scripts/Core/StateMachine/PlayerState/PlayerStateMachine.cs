using System.Collections;
using System.Collections.Generic;
using Core.StateMachine;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; }
    public void Initialize(PlayerState state)
    {
        currentState = state;
        currentState.StateBegin();
    }

    public void ChangeState(PlayerState newState)
    {
        currentState.StateEnd();
        currentState = newState;
        currentState.StateBegin();
    }
}