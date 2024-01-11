using System;
using System.Collections;
using System.Collections.Generic;
using Core.Components;
using UnityEngine;

namespace Core.Entity
{
    [RequireComponent(typeof(RigidbodyComponent))]
    public class Character : MonoBehaviour
    {
        public RigidbodyComponent rb { get; private set; }
        public Animator animator { get; private set; }
        /// <summary>
        ///     Setup the entity's initial data when the game begins
        /// </summary>
        protected virtual void InitialSetup()
        {
            rb = GetComponent<RigidbodyComponent>();
            animator = GetComponentInChildren<Animator>();
        }

        protected virtual void Start()
        {
            InitialSetup();
        }

        private void OnValidate()
        {
            Animator animator = GetComponentInChildren<Animator>();
            if (!animator)
            {
                Debug.Log("Missing Animator Component in child object");
            }
        }
    }
}

