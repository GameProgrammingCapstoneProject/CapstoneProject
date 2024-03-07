using System.Collections;
using System.Collections.Generic;
using Core.Components;
using Core.Entity;
using Core.PlayerInput;
using UnityEngine;

namespace Core.StateMachine
{
    public class PlayerAirAttackState : PlayerState
    {
        private int _comboAttack;
        private const int _numberOfAttacks = 2;
        private float _lastTimeAttacked;
        private float _comboCooldown = 0.6f;
        private static readonly int AirComboAttack = Animator.StringToHash("AirComboAttack");
        private const float _busyTime = 0.1f;
        private const float _airAttackTimer = 0.1f;

        public PlayerAirAttackState(Player inputPlayer, string inputAnimName) : base(inputPlayer, inputAnimName)
        {
        }
        
        public override void StateBegin()
        {
            base.StateBegin();

            Object.FindObjectOfType<SoundManager>().Play("PlayerAirAttack");
            if (_comboAttack > _numberOfAttacks || Time.time >= _lastTimeAttacked + _comboCooldown)
                _comboAttack = 0;
        
            Player.animator.SetInteger(AirComboAttack, _comboAttack);
            
            float attackDirection = (Player.rb.CurrentFacingDirection == RigidbodyComponent.FacingDirections.RIGHT) ? 1 : -1;
            if (PlayerInputReader.Instance.movementAxis != 0)
                attackDirection = (Mathf.Approximately(PlayerInputReader.Instance.movementAxis, 1)) ? 
                    1 : -1;
            
            Player.rb.SetVelocity(attackDirection, Player.rb.velocity.y);
            StateTimer = _airAttackTimer;
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            if (StateTimer < 0)
                Player.rb.SetVelocity(0, Player.rb.velocity.y);
            if (AnimationEndTrigger)
                Player.States.stateMachine.ChangeState(Player.States.airState);
        }

        public override void StateEnd()
        {
            base.StateEnd();
            Player.StartCoroutine(nameof(Player.BusyFor), _busyTime);
            _comboAttack++;
            _lastTimeAttacked = Time.time;
        }


    }
}

