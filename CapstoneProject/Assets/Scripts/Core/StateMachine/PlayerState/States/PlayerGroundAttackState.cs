using System.Collections;
using System.Collections.Generic;
using Core.Components;
using Core.Entity;
using Core.PlayerInput;
using UnityEngine;

namespace Core.StateMachine
{
    public class PlayerGroundAttackState : PlayerState
    {
        private int _comboAttack;
        private float _lastTimeAttacked;
        private float _comboCooldown = 1;
        private const int _numberOfAttacks = 2;
        private const float _busyTime = 0.15f;
        private Vector2[] _attackOffsetMovement = { new Vector2(2, 0f), new Vector2(1, 0.5f), new Vector2(3, 0) };
        private const float _groundAttackTimer = 0.1f;
        
        public PlayerGroundAttackState(Player inputPlayer, string inputAnimName) : base(inputPlayer, inputAnimName)
        {
        }
        
        public override void StateBegin()
        {
            base.StateBegin();
          
            if (_comboAttack > _numberOfAttacks || Time.time >= _lastTimeAttacked + _comboCooldown)
                _comboAttack = 0;
            if (_comboAttack !=2)
                Object.FindObjectOfType<SoundManager>().Play("PlayerGroundAttack");
            else
                Object.FindObjectOfType<SoundManager>().Play("PlayerHeavyAttack");

            Player.animator.SetInteger("GroundComboAttack", _comboAttack);

            float attackDirection = (Player.rb.CurrentFacingDirection == RigidbodyComponent.FacingDirections.RIGHT) ? 1 : -1;
            if (PlayerInputReader.Instance.movementAxis != 0)
                attackDirection = (Mathf.Approximately(PlayerInputReader.Instance.movementAxis, 1)) ? 
                    1 : -1;
            Player.rb.SetVelocity(_attackOffsetMovement[_comboAttack].x * attackDirection, _attackOffsetMovement[_comboAttack].y);
            StateTimer = _groundAttackTimer;
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            if (StateTimer < 0)
                Player.rb.ResetToZeroVelocity();
            if (AnimationEndTrigger)
                Player.States.stateMachine.ChangeState(Player.States.idleState);
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

