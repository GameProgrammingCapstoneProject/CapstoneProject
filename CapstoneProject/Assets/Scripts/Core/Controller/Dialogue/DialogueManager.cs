using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    struct GameState
    {
        //Numbered by level: 1-5 for standard stages, 6 for a run completion, 0 for new game
        short recentDeath;

        //0 = default, no relation
        //1 = recently killed by player 
        //2 = completed sidequest 
        short relationshipStatus;

        //0 = none
        //1 = affiliated with Khai
        //2 = affiliated with Amaleth
        short affiliation;

        //Tracks the number of completed runs
        short runsCompleted;

        //Tracks the number of killed NPCs for Khai (negative), or 
        //NPC quests completed for Amaleth (positive)
        short playerProgress;

        //Tracks the progress of the current NPCs quest
        short NPCQuestProgress;

        //Tracks the current NPC loaded into the level
        //shopkeepers excluded; they are always loaded
        string currentNPC;

        //Tracks the current state of the dialogue box.
        short dialogueStage;
    };

    List <string> loadedDialogue = new List<string> ();
    TextMeshPro displayText;
    Canvas displayBox;
    Canvas displayPortrait;
    //Game object with transparency over main cam
    GameObject blurEffect;

    void Start()
    {
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


        return success;
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
