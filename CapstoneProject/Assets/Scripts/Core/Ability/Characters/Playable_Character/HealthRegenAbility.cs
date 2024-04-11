using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Health Regen Ability", fileName = "HealthRegenAbility", order = 3)]
public class HealthRegenAbility : PlayerAbility
{
    [SerializeField]
    private HealthRegen _healthRegenPrefab;
    private HealthRegen _currenthealthRegen;
    private readonly float _yOffset = 1f;
    [SerializeField] private int coinConsumeWhenUsing = 100;
    public event System.Action<HealthRegenAbility> OnHealthRegenAbilityCoolDown;
    protected override void Activate()
    {
        if (Instigator.CoinComponent.TryToConsumeCoins(coinConsumeWhenUsing))
        {
            if (_currenthealthRegen == null)
            {
                _currenthealthRegen = GameObject.Instantiate(_healthRegenPrefab, new Vector3(Instigator.transform.position.x, Instigator.transform.position.y + _yOffset, Instigator.transform.position.z), Instigator.transform.rotation);
                _currenthealthRegen.Setup(Instigator);
                OnHealthRegenAbilityCoolDown?.Invoke(this);
                Instigator.HealthComponent.IncreaseHealthBy(1);
            }
        }
    }
}
