using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.StateMachine
{
    public class EntityStateMachine<T> where T : EntityState
    {
        public T currentState { get; private set; }
        public void Initialize(T state)
        {
            currentState = state;
            currentState.StateBegin();
        }

        public void ChangeState(T newState)
        {
            currentState.StateEnd();
            currentState = newState;
            currentState.StateBegin();
        }
    }
}

