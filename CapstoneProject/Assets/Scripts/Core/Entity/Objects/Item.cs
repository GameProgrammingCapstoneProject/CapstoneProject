using System.Collections;
using System.Collections.Generic;
using Core.Gameplay;
using UnityEngine;

namespace Core.Entity
{
    public class Item : MonoBehaviour, IInteractable
    {
        public Animator animator { get; private set; }
        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            animator.SetBool("Idle", true);
        }

        public void Interact()
        {
            animator.SetTrigger("Open");
        }
    }
}

