using Core.Gameplay;
using UnityEngine;
public class Lever : MonoBehaviour, IInteractable
{
    public TriggerDoor[] doorsToOpen;
    public TriggerDoor[] doorsToClose;

    public void Interact()
    {
        foreach (TriggerDoor door in doorsToOpen)
        {
            if (door != null)
            {
                door.IsOpening = true;
            }
        }
        foreach (TriggerDoor door in doorsToClose)
        {
            if (door != null)
            {
                door.IsOpening = false;
            }
        }
    }
}