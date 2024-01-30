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

        public bool haveBeenInteracted { get; private set; } = false;

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            haveBeenInteracted = false;
            animator.SetBool("Idle", true);
        }

        public void Interact()
        {
            if (haveBeenInteracted) return;
            animator.SetBool("Idle", false);
            animator.SetTrigger("Open");
            haveBeenInteracted = true;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            
        }
    }
}

