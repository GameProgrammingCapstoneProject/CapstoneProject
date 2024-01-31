using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

public class BowShootingAbility : PlayerAbility
{
    private GameObject _arrowPrefab;
    private Transform _bowShootingPosition;
    private GameObject _currentArrow;
    private float _existDuration;
    private Transform _target;
    public event System.Action OnBowShootingAbilityCoolDown;
    public BowShootingAbility(Player player, float cooldown,
        GameObject arrowPrefab, Transform bowShootingPosition,
        float existDuration) : base(player, cooldown)
    {
        _arrowPrefab = arrowPrefab;
        _bowShootingPosition = bowShootingPosition;
        _existDuration = existDuration;
    }

    public override void Update()
    {
        base.Update();
        //TODO: Implement function which is searching for the lowest health target
        if (_target == null)
            _target = GameObject.Find("Goblin_Melee").transform;
    }

    protected override void Activate()
    {
        if (_currentArrow == null)
        {
            _currentArrow = GameObject.Instantiate(_arrowPrefab, _bowShootingPosition.position, Instigator.transform.rotation);
            Arrow arrow = _currentArrow.GetComponent<Arrow>();
            arrow.Setup(_existDuration, _bowShootingPosition.position, _target.position);
            OnBowShootingAbilityCoolDown?.Invoke();
        }
    }
}
