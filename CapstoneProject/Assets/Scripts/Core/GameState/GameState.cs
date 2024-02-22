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


        public short recentDeath;
        public short relationshipStatus;

        public short affiliation;
        public short runsCompleted;
        public short playerProgress;
        public short NPCQuestProgress;
        public string currentNPC;
        public short dialogueStage;

        public short ReturnRecentDeaths()
        {
            return recentDeath;
        }

        public short ReturnrelationshipStatus()
        {
            return relationshipStatus;
        }
        
        public short ReturnAffiliation()
        {
            return affiliation;
        }

        public short ReturnRunsCompleted()
        {
            return runsCompleted;
        }

        public short ReturnPlayerProgress()
        {
            return playerProgress;
        }

        public short ReturnNPCQuestProgress()
        {
            return NPCQuestProgress;
        }

        public string ReturnCurrentNPC()
        {
            return currentNPC;
        }

        public short ReturnDialogueStage()
        {
            return dialogueStage;
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

        protected override void Awake()
        {
            base.Awake();
            _currentGameState = States.Gameplay;
        }
    }
}

