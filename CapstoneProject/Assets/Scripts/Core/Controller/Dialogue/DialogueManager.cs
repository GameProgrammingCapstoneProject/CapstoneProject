using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86;

public class DialogueManager : MonoBehaviour
{

    //Handles the current player actions against the NPC
    public enum relationshipStatus { defaultState, killedByPlayer, killedByPlayerRepeat, sidequestFinished };
    //Handles the players affiliation for ending purposes
    public enum playerAffiliation { none, khai, amaleth }
    //Tracks the recent deaths of the player
    public enum recentDeaths { newGame, died, diedRepeat, runComplete }

    public struct GameState
    {
        public recentDeaths deathStatus;
        public relationshipStatus status;
        public playerAffiliation affiliation;

        //Tracks the number of completed runs
        public short runsCompleted;

        //Tracks the number of killed NPCs for Khai (negative), or 
        //NPC quests completed for Amaleth (positive)
        public short playerProgress;

        //Tracks the progress of the current NPCs quest
        public short NPCQuestProgress;

        //Tracks the current NPC associated with the dialogue manager
        //shopkeepers excluded
        public string currentNPC;
    };

    GameState gameState = new GameState();

    //Loaded dialogue objects
    List<string> loadedDialogue = new List<string> ();
    string dialogueTransition;

    //Referenced UI elements
    public GameObject displayTextObject;
    public GameObject displayBoxObject;
    public GameObject displayPortraitObject;

    //Needed UI components
    private UnityEngine.UI.Image displayBoxImage;
    private UnityEngine.UI.Image displayPortraitImage;
    private TextMeshProUGUI displayTextMeshPro;

    //Sets the NPC in the inspector to the desired type
    public enum npcID { Whirl, Emelia, Ezekiel };
    public npcID currentID;
   
    //Use to get the relevant UI elements
    //displayBoxObject.GetComponent<Image>();
    //displayPortraitObject.GetComponent<Image>();
    //displayTextObject.GetComponent<TextMeshPro>()

    //Game object with transparency over main cam
    public GameObject vignetteEffect;

    //Referenced scriptable objects for dialogue
    public DialogueExample whirlDialogue;
    public DialogueExample emeliaDialogue;
    public DialogueExample ezekielDialogue;

    //Images for text and dialogue portrait
    public Sprite textBoxImage;
    public Sprite whirlPortraitImage;
    public Sprite emeliaPortraitImage;
    public Sprite ezekielPortraitImage;

    //Text scrolling variables
    private float textScrollSpeed = 0.06f;

    //Player input variables
    private bool dialoguePriority = false;
    private bool dialogueIsplaying = false;
    private short dialogueState = 0;
    private bool dialogueSkipRepeat = false;

    //Reference of the scrolling text coroutine to stop it
    private IEnumerator scrollingCoroutine;

    void Start()
    {
        //Debug for game state
        //gameState.currentNPC = "Whirl";

        //Default values for the game state
        gameState.deathStatus = recentDeaths.newGame;
        gameState.status = relationshipStatus.defaultState;
        gameState.affiliation = playerAffiliation.none;

        if (currentID == npcID.Whirl)
        {
            gameState.currentNPC = "Whirl";
        }
        else if (currentID == npcID.Emelia)
        {
            gameState.currentNPC = "Emelia";
        }
        else if (currentID == npcID.Ezekiel)
        {
            gameState.currentNPC = "Ezekiel";
        }

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
                //Gets the required components of each object
                displayBoxImage = displayBoxObject.GetComponent<UnityEngine.UI.Image>();
                displayPortraitImage = displayPortraitObject.GetComponent<UnityEngine.UI.Image>();
                displayTextMeshPro = displayTextObject.GetComponent<TextMeshProUGUI>();
                
                UnityEngine.Debug.Log("Successfully loaded dialogue.");
                //StartInteraction();
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
        else if (gameState.currentNPC == "Ezekiel")
        {
            MoveObjectToList(ezekielDialogue);
            displayPortraitObject.GetComponent<UnityEngine.UI.Image>().sprite = ezekielPortraitImage;
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
        if (!dialogueIsplaying)
        {
            dialogueIsplaying = true;
            UnityEngine.Debug.Log("Starting interaction...");
            LoadScriptObject();
            DisplayDialogueBox();
            DisplayDialoguePortrait();
            BeginText();
            vignetteEffect.GetComponent<VignetteController>().SetActiveBlack();
            //StartCoroutine(TextScrollInput("Wagagabobo"));

            StartCoroutine(MainDialogueLoop());
        }


    }


    //Enables both UI elements
    private void DisplayDialogueBox()
    {


        displayBoxImage.enabled = true;

    }

    private void DisplayDialoguePortrait()
    {

        displayPortraitImage.enabled = true;

    }

    private void BeginText()
    {
        displayTextMeshPro.enabled = true;

        //scrollingCoroutine = TextScrollInput("This is test dialogue.");
        //StartCoroutine(scrollingCoroutine);

    }

    private IEnumerator MainDialogueLoop()
    {
        IEnumerator textCoroutine = null;
        
        UnityEngine.Debug.Log("Main dialogue loop entered");


        foreach(string dialogue in loadedDialogue)
        {
            UnityEngine.Debug.Log("Looping");
            dialogueSkipRepeat = false;

            if (gameState.status == relationshipStatus.killedByPlayerRepeat)
            {
                StartCoroutine(SearchForState(textCoroutine, "playerkillrepeat", "break"));
                dialogueSkipRepeat = true;
            }
            else if (gameState.status == relationshipStatus.killedByPlayer)
            {
                StartCoroutine(SearchForState(textCoroutine, "playerkill", "playerdeathrepeat"));
                dialogueSkipRepeat = true;
            }
            else if (gameState.deathStatus == recentDeaths.died)
            {
                StartCoroutine(SearchForState(textCoroutine, "playerdeath", "playerkill"));
                dialogueSkipRepeat = true;
            }
            else if (gameState.deathStatus == recentDeaths.diedRepeat)
            {
                StartCoroutine(SearchForState(textCoroutine, "playerdeathrepeat", "playerkillrepeat"));
                dialogueSkipRepeat = true;
            }
            
            if (dialogueSkipRepeat || dialogueState == 6)
            {                                                       
                UnityEngine.Debug.Log("Drawing text 2");                
                textCoroutine = TextScrollInput(dialogueTransition);
                dialoguePriority = true;
                StartCoroutine(textCoroutine);
                while (dialoguePriority)
                {
                    yield return new WaitForSeconds(0.2F);
                }
                UnityEngine.Debug.Log("Finished drawing text 2");
                Cleanup();
                break;
            }

            if (dialogue == "maindialogue1" && dialogueState == 0)
            {
                UnityEngine.Debug.Log("Began sending dialogue 1");
                StartCoroutine(SearchForState(textCoroutine, "maindialogue1", "maindialogue2"));
                dialogueState++;
                UnityEngine.Debug.Log("finished dialogue");
                break;
            }
            else if (dialogue == "maindialogue2" && dialogueState == 1)
            {
                StartCoroutine(SearchForState(textCoroutine, "maindialogue2", "maindialogue3"));
                dialogueState++;
                break;
            }
            else if (dialogue == "maindialogue3" && dialogueState == 2)
            {
                StartCoroutine(SearchForState(textCoroutine, "maindialogue3", "maindialogue4"));
                dialogueState++;
                break;
            }
            else if (dialogue == "maindialogue4" && dialogueState == 3)
            {
                StartCoroutine(SearchForState(textCoroutine, "maindialogue4", "maindialogue5"));
                dialogueState++;
                break;
            }
            else if (dialogue == "maindialogue5" && dialogueState == 4)
            {
                StartCoroutine(SearchForState(textCoroutine, "maindialogue5", "maindialogue6"));
                dialogueState++;
                break;
            }
            else if (dialogue == "maindialogue6" && dialogueState == 5)
            {
                StartCoroutine(SearchForState(textCoroutine, "maindialogue6", "playerdeath"));
                dialogueState++;
                break;
            }

            /*else if (!dialogueSkipRepeat)
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
            }*/
        }
        UnityEngine.Debug.Log(dialogueIsplaying);
    }

    private IEnumerator TextScrollInput(string displayText)
    {
        IEnumerator textCoroutine = TextScroll(displayText);
        StartCoroutine(textCoroutine);
        UnityEngine.Debug.Log("In the input enumerator");

        bool input = false;
        while (!input)
        {
            if (Input.GetKeyUp(KeyCode.R))
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
        bool breakText = false;
        foreach (string dialogueStatus in loadedDialogue)
        {
            if (dialogueStatus == searchText)
            {
                UnityEngine.Debug.Log("found the text");
                int position = loadedDialogue.IndexOf(dialogueStatus);
                foreach (string dialogueStatusConfirmed in loadedDialogue)
                {
                    if (dialogueStatusConfirmed == checkText)
                    {
                        UnityEngine.Debug.Log("breaking");
                        breakText = true;
                        break;
                    }
                    if (loadedDialogue.IndexOf(dialogueStatusConfirmed) > position)
                    {

                        UnityEngine.Debug.Log("Drawing text 1");
                        textCoroutine = TextScrollInput(dialogueStatusConfirmed);
                        dialoguePriority = true;
                        StartCoroutine(textCoroutine);
                        while (dialoguePriority)
                        {
                            yield return new WaitForSeconds(0.2F);
                        }
                        UnityEngine.Debug.Log("Finished drawing text 1");
                        StopCoroutine(textCoroutine);
                    }
                }
            }
            if (breakText)
            {
                break;
            }
            //gameState.relationshipStatus = 0;
        }
        Cleanup();
    }

    private IEnumerator TextScroll(string displayText)
    {
        UnityEngine.Debug.Log("Writing text with length of" + displayText.Length);
        for (int i = 0; i < displayText.Length; i++)
        {
            displayTextObject.GetComponent<TextMeshProUGUI>().SetText(displayText.Substring(0, i+1));
            yield return new WaitForSeconds(textScrollSpeed);
        }
    }

    private void Cleanup()
    {
        StopAllCoroutines();
        if (displayPortraitObject != null)
        {
            displayPortraitImage.enabled = false;
        }
        if (displayBoxObject != null)
        {
            displayBoxImage.enabled = false;
        }
        if (displayTextObject != null)
        {
            displayTextMeshPro.enabled = false;
        }
        if (vignetteEffect != null)
        {
            vignetteEffect.GetComponent<VignetteController>().DisableVignette();
        }
        dialogueIsplaying = false;
        GetComponent<NPCDialogue>().ResetTriggerFlag();
    }
    private void OnDestroy()
    {
        Cleanup();
    }

}