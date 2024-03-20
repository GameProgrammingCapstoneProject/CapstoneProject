using System.Collections;
using System.Collections.Generic;
using Core.Gameplay;
using Core.GameStates;
using UnityEngine;

public class NPCDialogue : MonoBehaviour, IInteractable
{
    bool triggered = false;
    public void Interact()
    {
        if (!triggered)
        {
            Debug.Log("Interacted!");
            GetComponent<DialogueManager>().StartInteraction();
            triggered = true;
        }
        
    }

    public void ResetTriggerFlag()
    {
        triggered = false;
    }

}
