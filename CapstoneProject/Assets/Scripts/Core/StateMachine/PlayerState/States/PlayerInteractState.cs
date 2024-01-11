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
            /*Interact();*/
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            /*if (AnimationEndTrigger)
                Player.stateMachine.ChangeState(Player.idleState);*/
        }

        public override void StateEnd()
        {
            base.StateEnd();
        }

        public void Interact()
        {
        
        }
    }
}

