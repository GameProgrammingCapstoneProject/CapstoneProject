using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Dash Ability", fileName = "DashAbility", order = 0)]
public class DashAbility : PlayerAbility
{
    public event System.Action<DashAbility> OnDashAbilityCoolDown;
    protected override void Activate()
    {
        Debug.Log("Dash Ability Activated!!!");
        OnDashAbilityCoolDown?.Invoke(this);
        //TODO: Make the player invincible
    }
}
