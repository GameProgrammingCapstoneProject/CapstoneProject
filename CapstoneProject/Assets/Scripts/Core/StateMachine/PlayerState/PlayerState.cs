using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.StateMachine;
using UnityEngine;

namespace Core.StateMachine
{
    public class PlayerState : EntityState
    {
        protected Player Player;
        protected float HorizontalInput;
        protected float VerticalInput;
        private string _animBoolName;
        protected float StateTimer;
        protected bool AnimationEndTrigger = false;
        public PlayerState(Player inputPlayer, string inputAnimName) : base(inputAnimName)
        {
            this.Player = inputPlayer;
            this._animBoolName = inputAnimName;
        }
        public override void StateBegin()
        {
            base.StateBegin();
            Player.animator.SetBool(_animBoolName, true);
            AnimationEndTrigger = false;
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            StateTimer -= Time.deltaTime;
            HorizontalInput = Input.GetAxisRaw("Horizontal");
            VerticalInput = Input.GetAxisRaw("Vertical");
            Player.animator.SetFloat("VerticalVelocity", Player.rb.velocity.y);
        }

        public override void StateEnd()
        {
            base.StateEnd();
            Player.animator.SetBool(_animBoolName, false);
        }

        public void EndAnimationTrigger()
        {
            AnimationEndTrigger = true;
        }
    }
}

