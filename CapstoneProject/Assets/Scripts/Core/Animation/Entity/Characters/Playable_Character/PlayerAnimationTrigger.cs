using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

public class PlayerAnimationTrigger : EntityAnimationTrigger<Player>
{
    private void EndAttackAnimationTrigger()
    {
        _theEntity.EndAnimationTrigger();
    }
}
