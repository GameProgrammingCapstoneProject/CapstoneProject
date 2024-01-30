using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

public class BowShootingAbility : PlayerAbility
{
    [SerializeField]
    private GameObject _arrowPrefab;
    [SerializeField]
    private Transform _bowShootingPosition;
    private GameObject _currentArrow;
    [SerializeField]
    private float _existDuration = 3f;
    [SerializeField]
    private Transform _target;
    public event System.Action OnBowShootingAbilityCoolDown;
    protected override void Activate()
    {
        if (_currentArrow == null)
        {
            Player player = GetComponent<Player>();
            _currentArrow = Instantiate(_arrowPrefab, _bowShootingPosition.position, player.transform.rotation);
            Arrow arrow = _currentArrow.GetComponent<Arrow>();
            arrow.Setup(_existDuration, _bowShootingPosition.position, _target.position);
            OnBowShootingAbilityCoolDown?.Invoke();
        }
    }
}
