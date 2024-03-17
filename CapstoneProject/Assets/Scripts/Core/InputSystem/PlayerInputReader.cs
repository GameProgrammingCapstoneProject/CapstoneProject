using System;
using Core.Extension;
using Core.GameStates;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.PlayerInput
{
    public class PlayerInputReader : PersistentObject<PlayerInputReader>
    {
        private PlayerInputActions _playerInputActions;
        private bool _isAttackingCooldown = false;
        private float _attackCooldownTime = 0.4f;
        public float movementAxis { get; private set; } = 0;
        public float jumpValue { get; private set; } = 0;
        public float attackValue { get; private set; } = 0;
        public float dashValue { get; private set; } = 0;
        public float interactValue { get; private set; } = 0;
        public float firstAbilityValue { get; private set; } = 0;
        public float secondAbilityValue { get; private set; } = 0;
        public float verticalAxis { get; private set; } = 0;
        public float backValueGameplay { get; private set; } = 0;
        public float backValueUI { get; private set; } = 0;
        public float confirmValueUI { get; private set; } = 0;
        public float firstSelectionAbilityUIValue { get; private set; } = 0;
        public float secondSelectionAbilityUIValue { get; private set; } = 0;
        protected override void Awake()
        {
            base.Awake();
            _playerInputActions = new PlayerInputActions();
        }

        protected override void Start()
        {
            base.Start();
            SubscribeToGameplayInputEvents();
            SubscribeToUIInputEvents();
            GameState.OnGameStateChanged += SwitchPlayerInputSystem;
            SwitchPlayerInputSystem(GameState.States.Gameplay);
        }

        private void Update()
        {
            jumpValue = 0;
            attackValue = 0;
            verticalAxis = 0;
            dashValue = 0;
            interactValue = 0;
            backValueGameplay = 0;
            backValueUI = 0;
            confirmValueUI = 0;
        }

        private void SwitchPlayerInputSystem(GameState.States newgamestate)
        {
            if (newgamestate == GameState.States.Gameplay)
            {
                _playerInputActions.UI.Disable();
                _playerInputActions.Gameplay.Enable();
            }
            else if (newgamestate == GameState.States.UI)
            {
                _playerInputActions.Gameplay.Disable();
                _playerInputActions.UI.Enable();
            }
            else if (newgamestate == GameState.States.CutScene)
            {
                _playerInputActions.Gameplay.Disable();
                _playerInputActions.UI.Disable();
            }
        }
        private void StartAttackCooldown()
        {
            if (!_isAttackingCooldown)
            {
                _isAttackingCooldown = true;
                Invoke(nameof(EndAttackCooldown), _attackCooldownTime); // Adjust the cooldown duration as needed
            }
        }
        private void EndAttackCooldown()
        {
            _isAttackingCooldown = false;
        }

        private void SubscribeToGameplayInputEvents()
        {
            _playerInputActions.Gameplay.Movement.started += (context) => movementAxis = context.ReadValue<float>();
            _playerInputActions.Gameplay.Movement.performed += (context) => movementAxis = context.ReadValue<float>();
            _playerInputActions.Gameplay.Movement.canceled += (context) => movementAxis = 0;
            _playerInputActions.Gameplay.Jump.started += (context) => jumpValue = context.ReadValue<float>();
            _playerInputActions.Gameplay.Jump.canceled += (context) => jumpValue = 0;
            _playerInputActions.Gameplay.Attack.started += (context) =>
            {
                if (!_isAttackingCooldown)
                {
                    attackValue = context.ReadValue<float>();
                    StartAttackCooldown();
                }
            };
            _playerInputActions.Gameplay.Attack.canceled += (context) => attackValue = 0;
            _playerInputActions.Gameplay.Dash.started += (context) => dashValue = context.ReadValue<float>();
            _playerInputActions.Gameplay.Dash.canceled += (context) => dashValue = 0;
            _playerInputActions.Gameplay.Interact.started += (context) => interactValue = context.ReadValue<float>();
            _playerInputActions.Gameplay.Interact.canceled += (context) => interactValue = 0;
            _playerInputActions.Gameplay.FirstAbility.started += (context) => firstAbilityValue = context.ReadValue<float>();
            _playerInputActions.Gameplay.FirstAbility.canceled += (context) => firstAbilityValue = 0;
            _playerInputActions.Gameplay.SecondAbility.started += (context) => secondAbilityValue = context.ReadValue<float>();
            _playerInputActions.Gameplay.SecondAbility.canceled += (context) => secondAbilityValue = 0;
            _playerInputActions.Gameplay.Pause.started += (context) => backValueGameplay = context.ReadValue<float>();
            _playerInputActions.Gameplay.Pause.canceled += (context) => backValueGameplay = 0;
        }

        private void SubscribeToUIInputEvents()
        {
            _playerInputActions.UI.VerticalSelection.started += (context) => verticalAxis = context.ReadValue<float>();
            _playerInputActions.UI.VerticalSelection.canceled += (context) => verticalAxis = 0;
            _playerInputActions.UI.Back.started += (context) => backValueUI = context.ReadValue<float>();
            _playerInputActions.UI.Back.canceled += (context) => backValueUI = 0;
            _playerInputActions.UI.Confirm.started += (context) => confirmValueUI = context.ReadValue<float>();
            _playerInputActions.UI.Confirm.canceled += (context) => confirmValueUI = 0;
            _playerInputActions.UI.FirstAbilitySelect.started += (context) => firstSelectionAbilityUIValue = context.ReadValue<float>();
            _playerInputActions.UI.FirstAbilitySelect.canceled += (context) => firstSelectionAbilityUIValue = 0;
            _playerInputActions.UI.SecondAbilitySelect.started += (context) => secondSelectionAbilityUIValue = context.ReadValue<float>();
            _playerInputActions.UI.SecondAbilitySelect.canceled += (context) => secondSelectionAbilityUIValue = 0;
        }

        private void OnDisable()
        {
            GameState.OnGameStateChanged -= SwitchPlayerInputSystem;
        }
    }
}
