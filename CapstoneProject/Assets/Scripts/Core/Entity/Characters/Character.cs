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
        // Reference Custom Rigidbody Component 
        public RigidbodyComponent rb;
        // Reference animator
        //TODO: Need to implement this in the Custom AnimationComponent
        public Animator animator;
        /// <summary>
        ///     Setup the entity's initial data when the game begins
        /// </summary>
        protected virtual void InitialSetup()
        {
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

