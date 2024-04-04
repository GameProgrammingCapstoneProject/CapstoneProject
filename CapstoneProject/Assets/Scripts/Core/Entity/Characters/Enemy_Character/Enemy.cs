using Core.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Entity
{
    public class Enemy : Character
    {
        public int enemyType;

        [SerializeField]
        float baseCastDistance = 0.5f;
        [SerializeField]
        Transform CastPos;
        
        public enum EnemyType
        {
            MELEE_ENEMY,
            RANGED_ENEMY,
            FLYING_ENEMY,
            CHASING_ENEMY,
            DASHING_ENEMY,

            NUM_ENEMIES
        }

        public enum AnimationState
        {
            IDLE,
            WALK,
            ATTACK,
            DASH,

            NUM_ANIMATION_STATES
        }

        protected override void Start()
        {
            base.Start();
        }

        public bool IsHittingWall()
        {
            bool result;

            // Define the cast distance for left and right
            float castingDistance = baseCastDistance;
            if (rb.CurrentFacingDirection == RigidbodyComponent.FacingDirections.LEFT)
            {
                castingDistance = -baseCastDistance;
            }

            // determine the target destination based on the cast distance
            Vector3 targetPos = CastPos.position;
            targetPos.x += castingDistance;

            Debug.DrawLine(CastPos.position, targetPos, Color.red);
            if (Physics2D.Linecast(CastPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        public bool IsHittingEdge()
        {
            bool result;

            // Define the cast distance for left and right
            float castingDistance = baseCastDistance;

            // determine the target destination based on the cast distance
            Vector3 targetPos = CastPos.position;
            targetPos.y -= castingDistance;

            Debug.DrawLine(CastPos.position, targetPos, Color.red);
            if (Physics2D.Linecast(CastPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return result;
        }
    }

}
