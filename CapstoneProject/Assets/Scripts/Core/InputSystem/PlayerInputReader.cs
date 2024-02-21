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
        public float movementAxis { get; private set; } = 0;
        public float jumpValue { get; private set; } = 0;
        public float attackValue { get; private set; } = 0;
        public float dashValue { get; private set; } = 0;
        public float interactValue { get; private set; } = 0;
        public float firstAbilityValue { get; private set; } = 0;
        public float secondAbilityValue { get; private set; } = 0;
        public float thirdAbilityValue { get; private set; } = 0;
        public float fourthAbilityValue { get; private set; } = 0;
        public float fifthAbilityValue { get; private set; } = 0;
        public float verticalAxis { get; private set; } = 0;
        public float backValue { get; private set; } = 0;
        public float confirmValue { get; private set; } = 0;
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
            GameState.OnGameStateChanged += SwitchPlayerInputSystem;
            SwitchPlayerInputSystem(GameState.States.Gameplay);
        }

        private void Update()
        {
            // TODO: Need to fix: There are bugs related to these values:
            // - (immediate perform a double jump)
            // - (when holding attack button, animation attack get stuck)
            jumpValue = 0;
            attackValue = 0;
            verticalAxis = 0;
            dashValue = 0;
            interactValue = 0;
            backValue = 0;
            confirmValue = 0;
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
                _playerInputActions.Gameplay.Dash.started += (context) => dashValue = context.ReadValue<float>();
                _playerInputActions.Gameplay.Dash.canceled += (context) => dashValue = 0;
                _playerInputActions.Gameplay.Interact.started += (context) => interactValue = context.ReadValue<float>();
                _playerInputActions.Gameplay.Interact.canceled += (context) => interactValue = 0;
                _playerInputActions.Gameplay.FirstAbility.started += (context) => firstAbilityValue = context.ReadValue<float>();
                _playerInputActions.Gameplay.FirstAbility.canceled += (context) => firstAbilityValue = 0;
                _playerInputActions.Gameplay.SecondAbility.started += (context) => secondAbilityValue = context.ReadValue<float>();
                _playerInputActions.Gameplay.SecondAbility.canceled += (context) => secondAbilityValue = 0;
                _playerInputActions.Gameplay.ThirdAbility.started += (context) => thirdAbilityValue = context.ReadValue<float>();
                _playerInputActions.Gameplay.ThirdAbility.canceled += (context) => thirdAbilityValue = 0;
                _playerInputActions.Gameplay.FourthAbility.started += (context) => fourthAbilityValue = context.ReadValue<float>();
                _playerInputActions.Gameplay.FourthAbility.canceled += (context) => fourthAbilityValue = 0;
                _playerInputActions.Gameplay.FifthAbility.started += (context) => fifthAbilityValue = context.ReadValue<float>();
                _playerInputActions.Gameplay.FifthAbility.canceled += (context) => fifthAbilityValue = 0;
                _playerInputActions.Gameplay.Pause.started += (context) => backValue = context.ReadValue<float>();
                _playerInputActions.Gameplay.Pause.canceled += (context) => backValue = 0;
            }
            else
            {
                //TODO: Implement later
                _playerInputActions.Gameplay.Disable();
                _playerInputActions.UI.Enable();
                _playerInputActions.UI.VerticalSelection.started += (context) => verticalAxis = context.ReadValue<float>();
                _playerInputActions.UI.VerticalSelection.canceled += (context) => verticalAxis = 0;
                _playerInputActions.UI.Back.started += (context) => backValue = context.ReadValue<float>();
                _playerInputActions.UI.Back.canceled += (context) => backValue = 0;
                _playerInputActions.UI.Confirm.started += (context) => confirmValue = context.ReadValue<float>();
                _playerInputActions.UI.Confirm.canceled += (context) => confirmValue = 0;
                _playerInputActions.UI.FirstAbilitySelect.started += (context) => firstSelectionAbilityUIValue = context.ReadValue<float>();
                _playerInputActions.UI.FirstAbilitySelect.canceled += (context) => firstSelectionAbilityUIValue = 0;
                _playerInputActions.UI.SecondAbilitySelect.started += (context) => secondSelectionAbilityUIValue = context.ReadValue<float>();
                _playerInputActions.UI.SecondAbilitySelect.canceled += (context) => secondSelectionAbilityUIValue = 0;
            }
        }
    }
}
