using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.GameStates;
using Core.PlayerInput;
using Core.StateMachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player _player;
    private PlayerStateComponent _playerStateComponent;
    private CollisionComponent _collisionComponent;
    private PlayerAbilityComponent _playerAbilityComponent;
    private PlayerState _currentState => _playerStateComponent.stateMachine.currentState;
    private void Awake()
    {
        _playerStateComponent = GetComponent<PlayerStateComponent>();
        _collisionComponent = GetComponent<CollisionComponent>();
        _playerAbilityComponent = GetComponent<PlayerAbilityComponent>();
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameState.Instance.CurrentGameState != GameState.States.Gameplay) return;
        switch (_currentState)
        {
            case PlayerIdleState:
            {
                if (IsPressed(PlayerInputReader.Instance.movementAxis))
                    _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.runState);
                if (IsPressed(PlayerInputReader.Instance.jumpValue))
                    _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.jumpState);
                if (IsPressed(PlayerInputReader.Instance.attackValue))
                    _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.groundAttackState);
                if (IsPressed(PlayerInputReader.Instance.dashValue))
                {
                    if (_playerAbilityComponent.dashAbility.CanUseSkill())
                    {
                        _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.dashState);
                    }
                }
                if (IsPressed(PlayerInputReader.Instance.interactValue) && _collisionComponent.CanInteract)
                    _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.interactState);
                if (IsPressed(PlayerInputReader.Instance.firstAbilityValue) && _playerAbilityComponent.playerAbilities[0] != null)
                {
                    if (_playerAbilityComponent.playerAbilities[0].CanUseSkill())
                    {
                        ExecuteAbilityState(_playerAbilityComponent.playerAbilities[0]);
                    }
                }
                break;
            }
            case PlayerRunState:
            {
                if (IsPressed(PlayerInputReader.Instance.jumpValue))
                    _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.jumpState);
                if (IsPressed(PlayerInputReader.Instance.attackValue))
                    _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.groundAttackState);
                if (IsPressed(PlayerInputReader.Instance.dashValue))
                {
                    if (_playerAbilityComponent.dashAbility.CanUseSkill())
                    {
                        _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.dashState);
                    }
                }
                break;
            }
            case PlayerJumpState:
            {
                if (IsPressed(PlayerInputReader.Instance.attackValue))
                    _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.airAttackState);
                if (IsPressed(PlayerInputReader.Instance.jumpValue) && _player.canDoubleJump)
                    _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.doubleJumpState);
                break;
            }
            case PlayerAirState:
            {
                if (IsPressed(PlayerInputReader.Instance.attackValue))
                    _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.airAttackState);
                if (IsPressed(PlayerInputReader.Instance.jumpValue) && _player.canDoubleJump)
                    _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.doubleJumpState);
                break;
            }
            case PlayerDoubleJumpState:
            {
                if (IsPressed(PlayerInputReader.Instance.attackValue))
                    _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.airAttackState);
                break;
            }
            case PlayerWallSlideState:
            {
                if (IsPressed(PlayerInputReader.Instance.jumpValue))
                    _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.wallJumpState);
                break;
            }
        }
    }

    private bool IsPressed(float value)
    {
        return value != 0;
    }
    private bool IsPressed(Vector2 value)
    {
        return value != Vector2.zero;
    }

    private void ExecuteAbilityState(PlayerAbility ability)
    {
        switch (ability)
        {
            case ShieldAbility:
            {
                _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.shieldAbilityState);
                break;
            }
        }
    }
}
