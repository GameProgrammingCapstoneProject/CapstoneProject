using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Bow Shooting Ability", fileName = "BowShootingAbility", order = 2)]
public class BowShootingAbility : PlayerAbility
{
    [SerializeField]
    private Arrow _arrowPrefab;
    private Arrow _currentArrow;
    [SerializeField]
    private float _existDuration;
    private Transform _target;
    public event System.Action OnBowShootingAbilityCoolDown;
    
    public override void AbilityUpdate()
    {
        base.AbilityUpdate();
        //TODO: Implement function which is searching for the lowest health target
        if (_target == null)
            _target = GameObject.Find("Goblin_Melee").transform;
    }

    protected override void Activate()
    {
        if (_currentArrow == null)
        {
            _currentArrow = GameObject.Instantiate(_arrowPrefab, Instigator.bowShootingPosition.transform.position, Instigator.transform.rotation);
            _currentArrow.Setup(_existDuration, Instigator.bowShootingPosition.transform.position, _target.position);
            OnBowShootingAbilityCoolDown?.Invoke();
        }
    }
}
