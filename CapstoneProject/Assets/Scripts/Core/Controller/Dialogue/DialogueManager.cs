using Panda.Examples.PlayTag;
using Panda.Examples.Shooter;
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

    //Needed UI components
    private UnityEngine.UI.Image displayBoxImage;
    private UnityEngine.UI.Image displayPortraitImage;
    private TextMeshProUGUI displayTextMeshPro;

    public int npcID = 0;
   
   
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
    private float textScrollSpeed = 0.1f;
    private bool textIsPlaying = false;

    //Player input variables
    private bool dialoguePriority = false;
    private bool dialogueIsplaying = false;
    private short dialogueState = 0;
    private bool dialogueSkipRepeat = false;

    private bool dialoguePlaying = false;

    //Reference of the scrolling text coroutine to stop it
    private IEnumerator scrollingCoroutine;

    void Start()
    {
        //Debug for game state
        //gameState.currentNPC = "Whirl";

        if (npcID == 0)
        {
            gameState.currentNPC = "Whirl";
        }
        else if (npcID == 1)
        {
            gameState.currentNPC = "Emelia";
        }
        else if (npcID == 2)
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
            
            if (dialogueSkipRepeat || dialogueState == 4)
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
                StopCoroutine(textCoroutine);
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
                StartCoroutine(SearchForState(textCoroutine, "maindialogue4", "playerdeath"));
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
        dialogueIsplaying = false;
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
            if (Input.GetKeyUp(KeyCode.E))
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
                UnityEngine.Debug.Log("found the text");
                int position = loadedDialogue.IndexOf(dialogueStatus);
                foreach (string dialogueStatusConfirmed in loadedDialogue)
                {
                    if (dialogueStatusConfirmed == checkText)
                    {
                        UnityEngine.Debug.Log("breaking");
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
        GetComponent<NPCDialogue>().ResetTriggerFlag();
    }
    private void OnDestroy()
    {
        Cleanup();
    }

}