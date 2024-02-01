using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.PlayerInput;
using Core.StateMachine;
using UnityEngine;

public class PlayerStateComponent : MonoBehaviour
{
    private Player _player;
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerRunState runState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerGroundAttackState groundAttackState { get; private set; }
    public PlayerAirAttackState airAttackState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerInteractState interactState { get; private set; }
    public PlayerDoubleJumpState doubleJumpState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerShieldAbilityState shieldAbilityState { get; private set; }
    public PlayerBowShootingState bowShootingState { get; private set; }
    public PlayerHealthRegenAbilityState healthRegenAbilityState { get; private set; }

    private void Awake()
    {
        _player = GetComponent<Player>();
        stateMachine = new PlayerStateMachine();
        
        idleState = new PlayerIdleState(_player, "Idle");
        runState = new PlayerRunState(_player, "Run");
        airState = new PlayerAirState(_player, "Jump");
        jumpState = new PlayerJumpState(_player, "Jump");
        groundAttackState = new PlayerGroundAttackState(_player, "GroundAttack");
        airAttackState = new PlayerAirAttackState(_player, "AirAttack");
        dashState = new PlayerDashState(_player, "Dash");
        interactState = new PlayerInteractState(_player, "Interact");
        doubleJumpState = new PlayerDoubleJumpState(_player, "DoubleJump");
        wallSlideState = new PlayerWallSlideState(_player, "WallSlide");
        wallJumpState = new PlayerWallJumpState(_player, "Jump");
        shieldAbilityState = new PlayerShieldAbilityState(_player, "ShieldAbility");
        bowShootingState = new PlayerBowShootingState(_player, "BowShootingAbility");
        healthRegenAbilityState = new PlayerHealthRegenAbilityState(_player, "HealthRegenAbility");
    }
    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.StateUpdate();
    }
}
