using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.StateMachine;
using UnityEngine;

namespace Core.StateMachine
{
    public class PlayerState
    {
        protected Player Player;
        protected string AnimName;
        protected float StateTimer;
        protected bool AnimationEndTrigger = false;
        private static readonly int VerticalVelocity = Animator.StringToHash("VerticalVelocity");

        public PlayerState(Player inputPlayer, string inputAnimName)
        {
            this.Player = inputPlayer;
            this.AnimName = inputAnimName;
        }
        public virtual void StateBegin()
        {
            Player.animator.SetBool(AnimName, true);
            AnimationEndTrigger = false;
        }

        //TODO: Implement this function like suggestions in this book: https://gameprogrammingpatterns.com/state.html
        // After implementing this function, then we can use the OnGroundState.
        public virtual void HandleInput()
        {
        }

        public virtual void StateUpdate()
        {
            StateTimer -= Time.deltaTime;
            Player.animator.SetFloat(VerticalVelocity, Player.rb.velocity.y);
        }

        public virtual void StateEnd()
        {
            Player.animator.SetBool(AnimName, false);
        }

        public void EndAnimationTrigger()
        {
            AnimationEndTrigger = true;
        }
    }
}

