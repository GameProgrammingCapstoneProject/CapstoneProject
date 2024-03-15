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
            SoundManager.Instance.Play("PlayerInteractSuccess");
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
          //  Object.FindObjectOfType<SoundManager>().Play("PlayerInteractFail");
        }

        public void Interact()
        {
            Player.CollisionComponent.targetItem.Interact();
            //TODO: Trigger a lever, pick up an item...
        }
    }
}

