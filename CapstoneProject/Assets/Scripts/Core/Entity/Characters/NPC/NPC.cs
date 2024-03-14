using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.Gameplay;
using Core.GameStates;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public Player player;
    [SerializeField]
    private GameObject _abilityShopUI;
    public void Interact()
    {
        player.SavePlayer();

        _abilityShopUI.SetActive(true);
        GameState.Instance.CurrentGameState = GameState.States.UI;
       // Object.FindObjectOfType<SoundManager>().Play("PlayerInteractSuccess");
    }
}
