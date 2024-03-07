using System.Collections;
using System.Collections.Generic;
using Core.Gameplay;
using Core.GameStates;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject _abilityShopUI;
    public void Interact()
    {
        //TODO: Trigger checkpoint save
      //  Object.FindObjectOfType<SoundManager>().Play("PlayerInteractSuccess");
        _abilityShopUI.SetActive(true);
        GameState.Instance.CurrentGameState = GameState.States.UI;
    }
}
