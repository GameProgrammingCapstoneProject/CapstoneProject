using System.Collections;
using System.Collections.Generic;
using Core.Components;
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
    public event System.Action<BowShootingAbility> OnBowShootingAbilityCoolDown;
    
    public override void AbilityUpdate()
    {
        base.AbilityUpdate();
        //TODO: Implement function which is searching for the lowest health target
    }

    protected override void Activate()
    {
        if (_currentArrow == null)
        {
            if (Instigator.AbilityComponent.ScanForNearestTarget())
            {
                _currentArrow = GameObject.Instantiate(_arrowPrefab, Instigator.bowShootingPosition.transform.position, Instigator.transform.rotation);
                _currentArrow.Setup(_existDuration, Instigator.bowShootingPosition.transform.position, Instigator.AbilityComponent.ScanForNearestTarget());
                OnBowShootingAbilityCoolDown?.Invoke(this);
            }
            else
            {
                Vector3 direction = Instigator.rb.CurrentFacingDirection == RigidbodyComponent.FacingDirections.RIGHT ? Vector3.right : Vector3.left;
                _currentArrow = GameObject.Instantiate(_arrowPrefab, Instigator.bowShootingPosition.transform.position, Instigator.transform.rotation);
                _currentArrow.Setup(_existDuration, Instigator.bowShootingPosition.transform.position, direction);
                OnBowShootingAbilityCoolDown?.Invoke(this);
            }
        }
    }
}
