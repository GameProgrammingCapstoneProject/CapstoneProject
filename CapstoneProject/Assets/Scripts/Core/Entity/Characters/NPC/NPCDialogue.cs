using System.Collections;
using System.Collections.Generic;
using Core.Gameplay;
using Core.GameStates;
using UnityEngine;

[RequireComponent(typeof(DialogueManager))]
public class NPCDialogue : MonoBehaviour, IInteractable
{
    bool triggered = false;
    DialogueManager manager;

    private void Start()
    {
        manager = GetComponent<DialogueManager>();
        if (manager == null )
        {
            Debug.LogError("Error: Failed to get Dialogue Manager of NPCDialogue component (Manager must be added to NPC)");
        }
    }

    public void Interact()
    {
        if (!triggered)
        {
            manager.StartInteraction();
            triggered = true;
        }
    }

    public void ResetTriggerFlag()
    {
        triggered = false;
    }

}
