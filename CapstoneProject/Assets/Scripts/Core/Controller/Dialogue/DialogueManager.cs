using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

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

    //Loaded dialogue object
    List <string> loadedDialogue = new List<string> ();
    string dialogueTransition;

    //Referenced UI elements
    public GameObject displayText;
    public GameObject displayBox;
    public GameObject displayPortrait;

    //Use to get the relevant UI elements
    //displayBox.GetComponent<Image>();
    //displayPortrait.GetComponent<Image>();
    //displayText.GetComponent<TextMeshPro>()

    //Game object with transparency over main cam
    public GameObject blurEffect;

    //Referenced scriptable objects for dialogue
    public DialogueExample whirlDialogue;
    public DialogueExample emeliaDialogue;

    void Start()
    {
        //Debug for game state
        gameState.currentNPC = "Whirl";

        //Loads and checks the display box sprite
        displayBox.GetComponent<Image>().sprite = Resources.Load<Sprite>("TextBox.png");
        if (displayBox.GetComponent<Image>().sprite == null)
        {
            UnityEngine.Debug.Log("Error: Failed to load image text box.");
        }

        //Loads from the required managers, and checks for success
        if (LoadStateFromGameManager() || LoadStateFromSaveManager())
        {
            //Loads from the scriptable objects
            if (LoadScriptObject())
            {
                UnityEngine.Debug.Log("Successfully loaded dialogue.");
            }
            else
            {
                UnityEngine.Debug.Log("Unexpected error when loading dialogue object in Dialogue Manager");
                Destroy(this);
            }
        }
        else
        {
            UnityEngine.Debug.Log("Unexpected error when loading state in Dialogue Manager");
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
            displayPortrait.GetComponent<Image>().sprite = Resources.Load<Sprite>("WhirlPortrait.png");
        }
        else if (gameState.currentNPC == "Emelia")
        {
            MoveObjectToList(emeliaDialogue);
            displayPortrait.GetComponent<Image>().sprite = Resources.Load<Sprite>("EmeliaPortrait.png");
        }


        //Check if the dialogue loaded successfully
        if (loadedDialogue == null)
        {
            UnityEngine.Debug.Log("Error: Failed to load dialogue.");
            success = false;
        }
        if (displayPortrait.GetComponent<Image>().sprite == null)
        {
            UnityEngine.Debug.Log("Error: Failed to load dialogue portrait.");
            success = false;
        }


        return success;
    }

    public void MoveObjectToList(DialogueExample dialogueObject)
    {
        //The first dialogue element is always the transition line.
        dialogueTransition = dialogueObject.dialogueText[0];

        //Moves the dialogue from the script to the list
        for (int i = 0; i < dialogueObject.dialogueText.Count - 1; i++)
        {
            loadedDialogue.Add(dialogueObject.dialogueText[i + 1]);
            //Debug.Log(loadedDialogue[i]);
        }

        //Prints out contents of both lists for debugging
        dialogueObject.dialogueText.ForEach(UnityEngine.Debug.Log);
        loadedDialogue.ForEach(UnityEngine.Debug.Log);
    }

    public void StartInteraction()
    {
        DisplayDialogueBox();
        DisplayDialoguePortrait();
        BeginText();
    }

    //Enables both UI elements
    private void DisplayDialogueBox()
    {
        displayBox.GetComponent<Image>().SetEnabled(true);
    }

    private void DisplayDialoguePortrait()
    {
        displayPortrait.GetComponent<Image>().SetEnabled(true);
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
