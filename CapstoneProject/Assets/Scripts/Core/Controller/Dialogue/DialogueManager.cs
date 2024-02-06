using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    public struct GameState
    {
        //Numbered by level: 1-5 for standard stages, 6 for a run completion, 0 for new game
        public short recentDeath;

        //0 = default, no relation
        //1 = recently killed by player 
        //2 = completed sidequest 
        public short relationshipStatus;

        //0 = none
        //1 = affiliated with Khai
        //2 = affiliated with Amaleth
        public short affiliation;

        //Tracks the number of completed runs
        public short runsCompleted;

        //Tracks the number of killed NPCs for Khai (negative), or 
        //NPC quests completed for Amaleth (positive)
        public short playerProgress;

        //Tracks the progress of the current NPCs quest
        public short NPCQuestProgress;

        //Tracks the current NPC loaded into the level
        //shopkeepers excluded; they are always loaded
        public string currentNPC;

        //Tracks the current state of the dialogue box.
        public short dialogueStage;
    };

    GameState gameState = new GameState();

    List <string> loadedDialogue = new List<string> ();
    string dialogueTransition = "";

    TextMeshPro displayText;
    Canvas displayBox;
    Canvas displayPortrait;
    //Game object with transparency over main cam
    GameObject blurEffect;

    public DialogueExample whirlDialogue;
    public DialogueExample emeliaDialogue;

    void Start()
    {
        //Debug for game state
        gameState.currentNPC = "Whirl";

        if (LoadStateFromGameManager() || LoadStateFromSaveManager())
        {
            if (LoadScriptObject())
            {
                Debug.Log("Successfully loaded dialogue.");
            }
            else
            {
                Debug.Log("Unexpected error when loading dialogue object in Dialogue Manager");
                Destroy(this);
            }
        }
        else
        {
            Debug.Log("Unexpected error when loading state in Dialogue Manager");
            Destroy(this);
        }
    }

    public bool LoadStateFromGameManager()
    {
        bool success = true;


        return success;
    }

    public bool LoadStateFromSaveManager()
    {
        bool success = true;


        return success;
    }


    public bool LoadScriptObject()
    {
        bool success = true;

        //Loads the dialogue based on the current scene NPC
        if (gameState.currentNPC == "Whirl")
        {
            MoveObjectToList(whirlDialogue);
        }
        else if (gameState.currentNPC == "Emelia")
        {
            MoveObjectToList(emeliaDialogue);
        }

        //Check if the dialogue loaded successfully
        if (loadedDialogue == null)
        {
            success = false;
        }

        return success;
    }

    public void MoveObjectToList(DialogueExample dialogueObject)
    {
        //The first dialogue element is always the transition line.
        dialogueTransition = dialogueObject.dialogueText[0];

        for (int i = 0; i < dialogueObject.dialogueText.Count - 1; i++)
        {
            loadedDialogue.Add(dialogueObject.dialogueText[i + 1]);
            Debug.Log(loadedDialogue[i]);
        }
        loadedDialogue.ForEach(Debug.Log);
    }

    public void StartInteraction()
    {
        DisplayDialogueBox();
        DisplayDialoguePortrait();
        BeginText();
    }

    private void DisplayDialogueBox()
    {

    }

    private void DisplayDialoguePortrait()
    {

    }

    private void BeginText()
    {


        Cleanup();
    }

    private void Cleanup()
    {

    }
    private void OnDestroy()
    {
        Cleanup();
    }

}
