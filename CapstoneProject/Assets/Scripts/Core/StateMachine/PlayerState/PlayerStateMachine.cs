using System.Collections;
using System.Collections.Generic;
using Core.StateMachine;
using UnityEngine;

namespace Core.StateMachine
{
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
            if (currentState == newState) return;
            currentState.StateEnd();
            currentState = newState;
            currentState.StateBegin();
        }
    }
}
