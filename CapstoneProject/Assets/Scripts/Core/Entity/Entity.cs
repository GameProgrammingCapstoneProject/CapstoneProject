using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Entity
{
    /// <summary>
    ///     A basic entity type which designed to be inherited by other entities.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer { get; private set; }

        protected virtual void Awake()
        {
        }
        protected virtual void OnEnable()
        {
        }

        protected virtual void Start()
        {
            InitialSetup();
        }
        
        protected virtual void Update()
        {
        }

        /// <summary>
        ///     Setup the entity's initial data when the game begins
        /// </summary>
        protected virtual void InitialSetup()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }
}

