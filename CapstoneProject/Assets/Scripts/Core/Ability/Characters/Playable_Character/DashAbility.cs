using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Dash Ability", fileName = "DashAbility", order = 0)]
public class DashAbility : PlayerAbility
{
    [SerializeField]
    private float _invincibleTime = 1.0f;
    public event System.Action<DashAbility> OnDashAbilityCoolDown;
    protected override void Activate()
    {
        Debug.Log("Dash Ability Activated!!!");
        AbilityHandler.StartRoutine(this);
        OnDashAbilityCoolDown?.Invoke(this);
    }

    public override IEnumerator ActivateRoutine()
    {
        Instigator.HealthComponent.MakeInvincible(true);
        yield return new WaitForSeconds(_invincibleTime);
        Instigator.HealthComponent.MakeInvincible(false);
    }
}
