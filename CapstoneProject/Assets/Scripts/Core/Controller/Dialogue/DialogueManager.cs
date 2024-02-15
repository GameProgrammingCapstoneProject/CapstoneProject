using Panda.Examples.PlayTag;
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
        //2 = killed by player more than once
        //3 = completed sidequest 
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
    private float textScrollSpeed = 0.1f;
    private bool textIsPlaying = false;

    //Player input variables
    private bool dialoguePriority = false;
    private bool dialogueIsplaying = false;
    private short dialogueState = 0;
    private short dialogueTicker = 0;
    private bool dialogueSkipRepeat = false;

    //Reference of the scrolling text coroutine to stop it
    private IEnumerator scrollingCoroutine;

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
                Cleanup();
                Destroy(this);
            }
        }
        else
        {
            UnityEngine.Debug.Log("Unexpected error when loading state in Dialogue Manager");
            Cleanup();
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
        //StartCoroutine(TextScrollInput("Wagagabobo"));

        StartCoroutine(MainDialogueLoop());
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
        //scrollingCoroutine = TextScrollInput("This is test dialogue.");
        //StartCoroutine(scrollingCoroutine);

    }

    private IEnumerator MainDialogueLoop()
    {
        dialogueIsplaying = true;
        
        IEnumerator textCoroutine = null;
        
        UnityEngine.Debug.Log("Main dialogue loop entered");




        foreach(string dialogue in loadedDialogue)
        {
            dialogueTicker++;
            UnityEngine.Debug.Log("Looping");

            if (gameState.relationshipStatus == 2)
            {
                StartCoroutine(SearchForState(textCoroutine, "playerkillrepeat", "break"));
                dialogueSkipRepeat = true;
            }
            else if (gameState.relationshipStatus == 1)
            {
                StartCoroutine(SearchForState(textCoroutine, "playerkill", "playerdeathrepeat"));
                dialogueSkipRepeat = true;
            }
            else if (gameState.recentDeath > 2)
            {
                StartCoroutine(SearchForState(textCoroutine, "playerkillrepeat", "break"));
                dialogueSkipRepeat = true;
            }
            else if (gameState.recentDeath == 1)
            {
                StartCoroutine(SearchForState(textCoroutine, "playerdeathrepeat", "playerkillrepeat"));
                dialogueSkipRepeat = true;
            }
            
            if (dialogueSkipRepeat)
            {
                UnityEngine.Debug.Log("Drawing text");
                textCoroutine = TextScrollInput(dialogueTransition);
                dialoguePriority = true;
                StartCoroutine(textCoroutine);
                while (dialoguePriority)
                {
                    yield return new WaitForSeconds(0.25F);
                }
                UnityEngine.Debug.Log("Finished drawing text");
                StopCoroutine(textCoroutine);
            }

            if (dialogue == "maindialogue1" && dialogueState != 1)
            {

            }

            else if (dialogue == "maindialogue1" || dialogue == "maindialogue2" || dialogue == "maindialogue3" || dialogue == "maindialogue4" || dialogue == "playerdeath")
            {
                dialogueState++;
                UnityEngine.Debug.Log("Dialogue state increased");
            }

            

            else if (!dialogueSkipRepeat)
            {
                UnityEngine.Debug.Log("Drawing text");
                textCoroutine = TextScrollInput(dialogue);
                dialoguePriority = true;
                StartCoroutine(textCoroutine);
                while (dialoguePriority)
                {
                    yield return new WaitForSeconds(0.25F);
                }
                UnityEngine.Debug.Log("Finished drawing text");
                StopCoroutine(textCoroutine);
            }
        }

        yield return null;
    }

    private IEnumerator TextScrollInput(string displayText)
    {
        IEnumerator textCoroutine = TextScroll(displayText);
        StartCoroutine(textCoroutine);
        UnityEngine.Debug.Log("In the input enumerator");

        bool input = false;
        while (!input)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                input = true;
                dialoguePriority = false;
            }
            yield return null;
        }
        StopCoroutine(textCoroutine);
    }

    private IEnumerator SearchForState(IEnumerator textCoroutine, string searchText, string checkText) 
    {
        foreach (string dialogueStatus in loadedDialogue)
        {
            if (dialogueStatus == searchText)
            {
                int position = loadedDialogue.IndexOf(dialogueStatus);
                foreach (string dialogueStatusConfirmed in loadedDialogue)
                {
                    if (dialogueStatusConfirmed == checkText)
                    {
                        break;
                    }
                    if (loadedDialogue.IndexOf(dialogueStatusConfirmed)! < position)
                    {
                        UnityEngine.Debug.Log("Drawing text");
                        textCoroutine = TextScrollInput(dialogueStatusConfirmed);
                        dialoguePriority = true;
                        StartCoroutine(textCoroutine);
                        while (dialoguePriority)
                        {
                            yield return new WaitForSeconds(0.25F);
                        }
                        UnityEngine.Debug.Log("Finished drawing text");
                        StopCoroutine(textCoroutine);
                    }
                }
            }
            gameState.relationshipStatus = 0;
        }
    }

    private IEnumerator TextScroll(string displayText)
    {
        UnityEngine.Debug.Log("Writing text");
        for (int i = 0; i < displayText.Length; i++)
        {
            
            displayTextObject.GetComponent<TextMeshProUGUI>().SetText(displayText.Substring(0, i+1));
            yield return new WaitForSeconds(textScrollSpeed);
        }
    }

    private void Cleanup()
    {
        if (displayPortraitObject != null)
        {
            displayPortraitObject.GetComponent<UnityEngine.UI.Image>().enabled = false;
        }
        if (displayBoxObject != null)
        {
            displayBoxObject.GetComponent<UnityEngine.UI.Image>().enabled = false;
        }
        if (displayTextObject != null)
        {
            displayTextObject.GetComponent<TextMeshProUGUI>().enabled = false;
        }
        if (displayTextObject != null)
        {
            displayTextObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        Cleanup();
    }

}