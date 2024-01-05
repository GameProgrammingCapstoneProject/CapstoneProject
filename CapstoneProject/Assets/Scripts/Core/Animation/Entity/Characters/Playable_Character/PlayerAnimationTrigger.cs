using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

namespace Core.Animation
{
    public class PlayerAnimationTrigger : EntityAnimationTrigger<Player>
    {
        private void EndAnimationTrigger()
        {
            _theEntity.EndAnimationTrigger();
        }
    }
}

