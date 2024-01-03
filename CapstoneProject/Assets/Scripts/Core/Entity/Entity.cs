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
        protected FacingDirections CurrentFacingDirection = FacingDirections.RIGHT;
        
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
    }
}

