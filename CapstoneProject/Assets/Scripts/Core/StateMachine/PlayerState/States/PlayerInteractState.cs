using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.Gameplay;
using Core.StateMachine;
using UnityEngine;

namespace Core.StateMachine
{
    public class PlayerInteractState : PlayerState, IInteractable
    {
        public PlayerInteractState(Player inputPlayer, string inputAnimName) : base(inputPlayer, inputAnimName)
        {
        }
        public override void StateBegin()
        {
            base.StateBegin();
            Interact();
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            if (AnimationEndTrigger)
                Player.States.stateMachine.ChangeState(Player.States.idleState);
        }

        public override void StateEnd()
        {
            base.StateEnd();
        }

        public void Interact()
        {
            Player.CollisionComponent.targetItem.Interact();
            //TODO: Trigger a lever, pick up an item...
        }
    }
}

