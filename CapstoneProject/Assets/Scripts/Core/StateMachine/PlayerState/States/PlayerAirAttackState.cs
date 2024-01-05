using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

namespace Core.StateMachine
{
    public class PlayerAirAttackState : PlayerState
    {
        private int _comboAttack;
        private float _lastTimeAttacked;
        private float _comboCooldown = 0.6f;
        
        public PlayerAirAttackState(Player inputPlayer, string inputAnimName) : base(inputPlayer, inputAnimName)
        {
        }
        
        public override void StateBegin()
        {
            base.StateBegin();
            HorizontalInput = 0;

            if (_comboAttack > 2 || Time.time >= _lastTimeAttacked + _comboCooldown)
                _comboAttack = 0;
        
            Player.animator.SetInteger("AirComboAttack", _comboAttack);
            
            float attackDirection = (Player.CurrentFacingDirection == Character.FacingDirections.RIGHT) ? 1 : -1;
            if (HorizontalInput != 0)
                attackDirection = (Mathf.Approximately(HorizontalInput, 1)) ? 
                    1 : -1;
            Player.SetVelocity(Player.rb.velocity.x * attackDirection, Player.rb.velocity.y);
            StateTimer = 0.1f;
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            if (StateTimer < 0)
                Player.SetVelocity(0, Player.rb.velocity.y);
            if (AnimationEndTrigger)
                Player.stateMachine.ChangeState(Player.airState);
        }

        public override void StateEnd()
        {
            base.StateEnd();
            Player.StartCoroutine(nameof(Player.BusyFor), 0.15f);
            _comboAttack++;
            _lastTimeAttacked = Time.time;
        }


    }
}

