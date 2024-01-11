using System;
using Core.Extension;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.PlayerInput
{
    public class PlayerInputReader : PersistentObject<PlayerInputReader>
    {
        private PlayerInputActions _playerInputActions;
        public float movementAxis { get; private set; } = 0;
        public float jumpValue { get; private set; } = 0;
        public float attackValue { get; private set; } = 0;
        

        protected override void Awake()
        {
            base.Awake();
            _playerInputActions = new PlayerInputActions();
        }

        protected override void Start()
        {
            base.Start();
            GameState.OnGameStateChanged += SwitchPlayerInputSystem;
            SwitchPlayerInputSystem(GameState.States.Gameplay);
        }

        private void SwitchPlayerInputSystem(GameState.States newgamestate)
        {
            if (newgamestate == GameState.States.Gameplay)
            {
                _playerInputActions.UI.Disable();
                _playerInputActions.Gameplay.Enable();
                _playerInputActions.Gameplay.Movement.started += (context) => movementAxis = context.ReadValue<float>();
                _playerInputActions.Gameplay.Movement.performed += (context) => movementAxis = context.ReadValue<float>();
                _playerInputActions.Gameplay.Movement.canceled += (context) => movementAxis = 0;
                _playerInputActions.Gameplay.Jump.started += (context) => jumpValue = context.ReadValue<float>();
                _playerInputActions.Gameplay.Jump.canceled += (context) => jumpValue = 0;
                _playerInputActions.Gameplay.Attack.started += (context) => attackValue = context.ReadValue<float>();
                _playerInputActions.Gameplay.Attack.canceled += (context) => attackValue = 0;
            }
            else
            {
                //TODO: Implement later
                _playerInputActions.Gameplay.Disable();
                _playerInputActions.UI.Enable();
                
            }
        }
    }
}
