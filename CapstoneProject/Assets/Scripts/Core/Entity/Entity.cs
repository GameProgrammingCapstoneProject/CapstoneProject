using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Entity
{
    /// <summary>
    ///     A basic entity type which designed to be inherited by other entities.
    /// </summary>
    public class Entity : MonoBehaviour
    {
        public enum FacingDirections
        {
            LEFT,
            RIGHT
        }
        
        // The direction that the entity is facing
        public FacingDirections CurrentFacingDirection = FacingDirections.RIGHT;
        public Rigidbody2D rb { get; private set; }
        public Animator animator { get; private set; }

        protected virtual void Awake()
        {
        }

        protected virtual void Start()
        {
            rb = GetComponentInChildren<Rigidbody2D>();
            animator = GetComponentInChildren<Animator>();
        }

        /// <summary>
        ///     Flip the sprite to the opposite direction.
        /// </summary>
        /// <value></value>
        public virtual void Flip()
        {
            CurrentFacingDirection = (CurrentFacingDirection == FacingDirections.RIGHT) ? 
                FacingDirections.LEFT : FacingDirections.RIGHT;
            transform.Rotate(0, 180, 0);
        }

        public void ResetToZeroVelocity()
        {
            rb.velocity = Vector2.zero;
        }
    }
}

