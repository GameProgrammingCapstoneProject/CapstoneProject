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
            GetComponent<DialogueManager>().StartInteraction();
            triggered = true;
        }
        //TODO: Trigger checkpoint save
        
    }
}
