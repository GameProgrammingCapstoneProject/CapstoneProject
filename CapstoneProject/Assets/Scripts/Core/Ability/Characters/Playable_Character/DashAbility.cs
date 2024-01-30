using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

public class DashAbility : PlayerAbility
{
    public event System.Action OnDashAbilityCoolDown;
    protected override void Activate()
    {
        Debug.Log("Dash Ability Activated!!!");
        OnDashAbilityCoolDown?.Invoke();
        //TODO: Make the player invincible
    }
}
