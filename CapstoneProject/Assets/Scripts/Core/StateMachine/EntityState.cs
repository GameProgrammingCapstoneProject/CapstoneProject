using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.StateMachine
{
    public class EntityState
    {
        protected string AnimName;

        public EntityState(string inputAnimName)
        {
            this.AnimName = inputAnimName;
        }

        public virtual void StateBegin()
        {
        }

        public virtual void StateUpdate()
        {
        }

        public virtual void StateEnd()
        {
        }
    }
}

