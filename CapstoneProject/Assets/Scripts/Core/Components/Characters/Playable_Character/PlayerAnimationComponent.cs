using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

namespace Core.Animation
{
    /// <summary>
    ///     A player animation component which is responsible for any
    ///     animation events or triggers.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationComponent : MonoBehaviour
    {
        // Player reference
        private Player _player;

        private void Awake()
        {
            _player = GetComponentInParent<Player>();
        }

        /// <summary>
        ///     This is a function that triggers at the end of the animation.
        ///     To notify that the animation is over, proceed to the next step
        /// </summary>
        private void EndAnimationTrigger()
        {
            _player.EndAnimationTrigger();
        }
        
    }
}

