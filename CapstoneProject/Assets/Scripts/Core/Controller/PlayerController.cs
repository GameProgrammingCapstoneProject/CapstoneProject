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
    [SerializeField]
    private AbilityShopUI _abilityShopUI;
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
        if (GameState.Instance.CurrentGameState == GameState.States.Gameplay)
        {
            if (IsPressed(PlayerInputReader.Instance.backValue))
            {
                if (PauseMenu.isPaused == true)
                {
                    PauseMenu.isPaused = false;
                    Debug.Log("Unpaused Game");
                }
                else
                {
                    PauseMenu.isPaused = true;
                    Debug.Log("Paused Game");
                }

            }
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
                        if (_playerAbilityComponent.DashAbility.CanUseAbility())
                            _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.dashState);
                    if (IsPressed(PlayerInputReader.Instance.interactValue) && _collisionComponent.CanInteract)
                        _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.interactState);
                    
                    HandleAbilityState();
                    break;
                }
                case PlayerRunState:
                {
                    if (IsPressed(PlayerInputReader.Instance.jumpValue))
                        _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.jumpState);
                    if (IsPressed(PlayerInputReader.Instance.attackValue))
                        _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.groundAttackState);
                    if (IsPressed(PlayerInputReader.Instance.dashValue))
                        if (_playerAbilityComponent.DashAbility.CanUseAbility())
                            _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.dashState);
                    HandleAbilityState();
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
        else
        {
            if (PlayerInputReader.Instance.verticalAxis < 0)
            {
                if (_abilityShopUI.CurrentSelectedAbility.index == _abilityShopUI.abilityInformation.Count - 1) return;
                _abilityShopUI.CurrentSelectedAbility =
                    _abilityShopUI.abilityInformation[_abilityShopUI.CurrentSelectedAbility.index + 1];
            }
            if (PlayerInputReader.Instance.verticalAxis > 0)
            {
                if (_abilityShopUI.CurrentSelectedAbility.index == 0) return;
                _abilityShopUI.CurrentSelectedAbility =
                    _abilityShopUI.abilityInformation[_abilityShopUI.CurrentSelectedAbility.index - 1];
            }
            if (IsPressed(PlayerInputReader.Instance.backValue))
            {
                GameState.Instance.CurrentGameState = GameState.States.Gameplay;
                _abilityShopUI.gameObject.SetActive(false);
            }
            if (IsPressed(PlayerInputReader.Instance.confirmValue))
            {
                //TODO: Need to implement coin component to unlock ability
                _abilityShopUI.CurrentSelectedAbility.information.Unlock();
            }
            if (IsPressed(PlayerInputReader.Instance.firstAbilityValue))
            {
                if (_abilityShopUI.CurrentSelectedAbility.information.IsAbilityUnlocked())
                    _playerAbilityComponent.SetupPlayerAbility(_abilityShopUI.CurrentSelectedAbility.information, 0);
            }
            if (IsPressed(PlayerInputReader.Instance.secondAbilityValue))
            {
                if (_abilityShopUI.CurrentSelectedAbility.information.IsAbilityUnlocked())
                    _playerAbilityComponent.SetupPlayerAbility(_abilityShopUI.CurrentSelectedAbility.information, 1);
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
            case BowShootingAbility:
            {
                _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.bowShootingState);
                break;
            }
            case HealthRegenAbility:
            {
                _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.healthRegenAbilityState);
                break;
            }
            case ProjectileShootingAbility:
            {
                _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.projectileShootingState);
                break;
            }
            case LightningStrikeAbility:
            {
                _playerStateComponent.stateMachine.ChangeState(_playerStateComponent.lightningStrikeState);
                break;
            }
        }
    }

    private void HandleAbilityState()
    {
        if (IsPressed(PlayerInputReader.Instance.firstAbilityValue) && _playerAbilityComponent.playerAbilities[0] != null)
        {
            if (_playerAbilityComponent.playerAbilities[0].CanUseAbility())
            {
                ExecuteAbilityState(_playerAbilityComponent.playerAbilities[0]);
            }
        }
        if (IsPressed(PlayerInputReader.Instance.secondAbilityValue) && _playerAbilityComponent.playerAbilities[1] != null)
        {
            if (_playerAbilityComponent.playerAbilities[1].CanUseAbility())
            {
                ExecuteAbilityState(_playerAbilityComponent.playerAbilities[1]);
            }
        }
    }
}
