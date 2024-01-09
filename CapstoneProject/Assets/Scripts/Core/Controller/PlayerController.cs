using System;
using System.Collections;
using System.Collections.Generic;
using Core.Command;
using Core.Entity;
using Core.PlayerInput;
using Core.StateMachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player _player;
    private PlayerState _currentState => _player.stateMachine.currentState;
    void Awake()
    {
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_currentState)
        {
            case PlayerIdleState:
            {
                if (PlayerInputSystem.Instance.movementAxis != 0)
                    _player.stateMachine.ChangeState(_player.runState);
                break;
            }
            case PlayerRunState:
            {
                break;
            }
        }
    }
}
