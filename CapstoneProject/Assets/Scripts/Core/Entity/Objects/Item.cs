using System.Collections;
using System.Collections.Generic;
using Core.Gameplay;
using UnityEngine;

namespace Core.Entity
{
    public class Item : Entity, IGameplayInterface
    {
        public Animator animator { get; private set; }
        protected override void Start()
        {
            base.Start();
            animator = GetComponentInChildren<Animator>();
            animator.SetBool("Idle", true);
        }

        public void Interact()
        {
            animator.SetTrigger("Open");
        }
    }
}

