using System;
using System.Collections;
using System.Collections.Generic;
using Core.Extension;
using UnityEngine;

namespace Core.GameStates
{
    public class GameState : PersistentObject<GameState>
    {
        public enum States
        {
            Gameplay,
            UI
        }

        // Define a delegate for the event
        public delegate void GameStateChangedHandler(States newGameState);
    
        // Define the event using the delegate
        public static event GameStateChangedHandler OnGameStateChanged;
    
        private States _currentGameState = States.Gameplay;

        public States CurrentGameState
        {
            get { return _currentGameState; }
            set
            {
                if (_currentGameState != value)
                {
                    _currentGameState = value;

                    // Trigger the event when the state changes
                    OnGameStateChanged?.Invoke(_currentGameState);
                }
            }
        }
    }
}

