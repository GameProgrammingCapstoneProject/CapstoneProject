using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Entity
{
    public class Character : Entity
    {
        public enum FacingDirections
        {
            LEFT,
            RIGHT
        }
        [Header("Movement information")]
        [SerializeField]
        private float _moveSpeed = 2f;
        // The direction that the entity is facing
        public FacingDirections CurrentFacingDirection { get; private set; } = FacingDirections.RIGHT;
        public Rigidbody2D rb { get; private set; }
        public Animator animator { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }
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
            rb.velocity = Vector2.zero;
        }
        
        /// <summary>
        ///     Set the entity's velocity
        /// </summary>
        public void SetVelocity(float horizontalVelocity, float verticalVelocity)
        {
            rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);
            ControlFlip(horizontalVelocity);
        }
        
        /// <summary>
        ///     Setup the entity's initial data when the game begins
        /// </summary>
        protected override void InitialSetup()
        {
            base.InitialSetup();
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponentInChildren<Animator>();
            SetMoveSpeed(_moveSpeed);
        }
        public float GetMoveSpeed() => _moveSpeed;
        public float SetMoveSpeed(float moveSpeed) => _moveSpeed = moveSpeed;

    }
}

