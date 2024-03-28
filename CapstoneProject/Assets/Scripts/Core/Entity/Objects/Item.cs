using System;
using System.Collections;
using System.Collections.Generic;
using Core.Gameplay;
using UnityEngine;

namespace Core.Entity
{
    public class Item : MonoBehaviour, IInteractable
    {
        public Animator animator { get; private set; }
        private Player _player;
        [SerializeField]
        private GameObject _coins;
        [SerializeField] private int _coinsInTheChest = 100;

        public bool haveBeenInteracted { get; private set; } = false;

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            haveBeenInteracted = false;
            animator.SetBool("Idle", true);
            _player = FindObjectOfType<Player>();
        }

        public void Interact()
        {
            if (haveBeenInteracted || _player.GetComponent<KeyItemComponent>().TryToUseKeys() == false) return;
            animator.SetBool("Idle", false);
            animator.SetTrigger("Open");
            haveBeenInteracted = true;
            _player.GetComponent<KeyItemComponent>().TryToUseKeys();
            _player.GetComponent<CoinComponent>().CollectCoins(_coinsInTheChest);
            _coins.SetActive(true);
        }
    }
}

