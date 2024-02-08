using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEditor;
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
    public GameObject displayTextObject;
    public GameObject displayBoxObject;
    public GameObject displayPortraitObject;

    //Use to get the relevant UI elements
    //displayBoxObject.GetComponent<Image>();
    //displayPortraitObject.GetComponent<Image>();
    //displayTextObject.GetComponent<TextMeshPro>()

    //Game object with transparency over main cam
    public GameObject blurEffect;

    //Referenced scriptable objects for dialogue
    public DialogueExample whirlDialogue;
    public DialogueExample emeliaDialogue;

    //Images for text and dialogue portrait
    public Sprite textBoxImage;
    public Sprite whirlPortraitImage;
    public Sprite emeliaPortraitImage;

    //Text scrolling variables
    private float textScrollSpeed = 0.2f;
    private bool textIsPlaying = false;

    void Start()
    {
        //Debug for game state
        gameState.currentNPC = "Whirl";

        //Loads and checks the display box sprite
        displayBoxObject.GetComponent<UnityEngine.UI.Image>().sprite = textBoxImage;
        if (displayBoxObject.GetComponent<UnityEngine.UI.Image>().sprite == null)
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
                StartInteraction();
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
            displayPortraitObject.GetComponent<UnityEngine.UI.Image>().sprite = whirlPortraitImage;
        }
        else if (gameState.currentNPC == "Emelia")
        {
            MoveObjectToList(emeliaDialogue);
            displayPortraitObject.GetComponent<UnityEngine.UI.Image>().sprite = emeliaPortraitImage;
        }


        //Check if the dialogue loaded successfully
        if (loadedDialogue == null)
        {
            UnityEngine.Debug.Log("Error: Failed to load dialogue.");
            success = false;
        }
        if (displayPortraitObject.GetComponent<UnityEngine.UI.Image>().sprite == null)
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
        //dialogueObject.dialogueText.ForEach(UnityEngine.Debug.Log);
        //loadedDialogue.ForEach(UnityEngine.Debug.Log);
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
        displayBoxObject.GetComponent<UnityEngine.UI.Image>().enabled = true;
    }

    private void DisplayDialoguePortrait()
    {
        displayPortraitObject.GetComponent<UnityEngine.UI.Image>().enabled = true;
    }

    private void BeginText()
    {
        displayTextObject.SetActive(true);
        displayTextObject.GetComponent<TextMeshProUGUI>().enabled = true;
        StartCoroutine(TextScroll("This is test dialogue. Wa ba ba go bo"));

        
    }

    IEnumerator TextScroll(string displayText)
    {
        for (int i = 0; i < displayText.Length; i++)
        {
            displayTextObject.GetComponent<TextMeshProUGUI>().SetText(displayText.Substring(0, i+1));
            yield return new WaitForSeconds(textScrollSpeed);
        }
        //Cleanup();

    }


    private void Cleanup()
    {
        displayPortraitObject.GetComponent<UnityEngine.UI.Image>().enabled = false;
        displayBoxObject.GetComponent<UnityEngine.UI.Image>().enabled = false;
        displayTextObject.GetComponent<TextMeshProUGUI>().enabled = false;
        displayTextObject.SetActive(false);
    }
    private void OnDestroy()
    {
        Cleanup();
    }

}
