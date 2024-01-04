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
        protected Rigidbody2D Rb;
        protected Animator Animator;
        protected float HorizontalInput;
        protected float VerticalInput;
        private string _animBoolName;
        public PlayerState(Player inputPlayer, string inputAnimName) : base(inputAnimName)
        {
            this.Player = inputPlayer;
            this._animBoolName = inputAnimName;
        }
        public override void StateBegin()
        {
            base.StateBegin();
            Rb = Player.rb;
            Animator = Player.animator;
            Animator.SetBool(_animBoolName, true);
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            HorizontalInput = Input.GetAxisRaw("Horizontal");
            VerticalInput = Input.GetAxisRaw("Vertical");
        }

        public override void StateEnd()
        {
            base.StateEnd();
            Animator.SetBool(_animBoolName, false);
        }
    }
}

