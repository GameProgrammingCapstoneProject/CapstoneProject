using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.PlayerInput;
using Core.StateMachine;
using UnityEngine;

public class PlayerStateComponent : MonoBehaviour
{
    public Player _player;
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerRunState runState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    /*public PlayerGroundAttackState groundAttackState { get; private set; }*/
    public PlayerAirAttackState airAttackState { get; private set; }
    /*public PlayerInteractState interactState { get; private set; }*/

    private void Awake()
    {
        _player = GetComponent<Player>();
        stateMachine = new PlayerStateMachine();
        
        idleState = new PlayerIdleState(_player, "Idle");
        runState = new PlayerRunState(_player, "Run");
        airState = new PlayerAirState(_player, "Jump");
        jumpState = new PlayerJumpState(_player, "Jump");
        /*groundAttackState = new PlayerGroundAttackState(this, "GroundAttack");*/
        airAttackState = new PlayerAirAttackState(_player, "AirAttack");
        /*interactState = new PlayerInteractState(this, "Interact");*/
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
