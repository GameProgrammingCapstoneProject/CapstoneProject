using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

public class DashAbility : PlayerAbility
{
    public event System.Action OnDashAbilityCoolDown;
    public DashAbility(Player player, float cooldown) : base(player, cooldown)
    {
    }
    protected override void Activate()
    {
        Debug.Log("Dash Ability Activated!!!");
        OnDashAbilityCoolDown?.Invoke();
        //TODO: Make the player invincible
    }
}
