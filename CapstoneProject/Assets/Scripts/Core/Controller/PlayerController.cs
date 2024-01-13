using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.PlayerInput;
using Core.StateMachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerStateComponent _playerStateComponent;
    private PlayerState _currentState => _playerStateComponent.stateMachine.currentState;
    private void Awake()
    {
        _playerStateComponent = GetComponent<PlayerStateComponent>();
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
                break;
            }
            case PlayerRunState:
            {
                if (IsPressed(PlayerInputReader.Instance.jumpValue))
                    _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.jumpState);
                break;
            }
            case PlayerAirState:
            {
                if (IsPressed(PlayerInputReader.Instance.attackValue))
                    _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.airAttackState);
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
}
