using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

namespace Core.StateMachine
{
    public class PlayerGroundAttackState : PlayerState
    {
        private int _comboAttack;
        private float _lastTimeAttacked;
        private float _comboCooldown = 1;
        private Vector2[] _attackOffsetMovement = { new Vector2(2, 0f), new Vector2(1, 0.5f), new Vector2(3, 0) };
        
        public PlayerGroundAttackState(Player inputPlayer, string inputAnimName) : base(inputPlayer, inputAnimName)
        {
        }
        
        public override void StateBegin()
        {
            base.StateBegin();
            if (_comboAttack > 2 || Time.time >= _lastTimeAttacked + _comboCooldown)
                _comboAttack = 0;
        
            Player.animator.SetInteger("GroundComboAttack", _comboAttack);

            float attackDirection = (Player.CurrentFacingDirection == Character.FacingDirections.RIGHT) ? 1 : -1;
            if (HorizontalInput != 0)
                attackDirection = (Mathf.Approximately(HorizontalInput, 1)) ? 
                    1 : -1;
            Player.SetVelocity(_attackOffsetMovement[_comboAttack].x * attackDirection, _attackOffsetMovement[_comboAttack].y);
            StateTimer = 0.1f;
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            if (StateTimer < 0)
                Player.ResetToZeroVelocity();
            if (AnimationEndTrigger)
                Player.stateMachine.ChangeState(Player.idleState);
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

