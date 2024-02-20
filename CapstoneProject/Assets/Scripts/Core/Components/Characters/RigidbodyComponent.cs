using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RigidbodyComponent : MonoBehaviour
    {
        public enum FacingDirections
        {
            LEFT,
            RIGHT
        }
        // The direction that the entity is facing
        public FacingDirections CurrentFacingDirection { get; private set; } = FacingDirections.RIGHT;
        [SerializeField]
        private Rigidbody2D _rb;

        public Vector2 velocity => _rb.velocity;
        
        /// <summary>
        ///     Flip the sprite to the opposite direction.
        /// </summary>
        private void Flip()
        {
            CurrentFacingDirection = (CurrentFacingDirection == FacingDirections.RIGHT) ? 
                FacingDirections.LEFT : FacingDirections.RIGHT;
            transform.Rotate(0, 180, 0);
        }
        
        /// <summary>
        ///     Control if the sprite should be flipped based on the Horizontal Velocity
        /// </summary>
        private void ControlFlip(float horizontalVelocity)
        {
            if (horizontalVelocity > 0 && CurrentFacingDirection == FacingDirections.LEFT)
            {
                Flip();
            }
            else if (horizontalVelocity < 0 && CurrentFacingDirection == FacingDirections.RIGHT)
            {
                Flip();
            }
        }
        
        /// <summary>
        ///     Reset the entity's velocity to 0
        /// </summary>
        public void ResetToZeroVelocity()
        {
            _rb.velocity = Vector2.zero;
        }
        
        /// <summary>
        ///     Set the entity's velocity
        /// </summary>
        public void SetVelocity(float horizontalVelocity, float verticalVelocity)
        {
            _rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);
            ControlFlip(horizontalVelocity);
        }
    }
}

