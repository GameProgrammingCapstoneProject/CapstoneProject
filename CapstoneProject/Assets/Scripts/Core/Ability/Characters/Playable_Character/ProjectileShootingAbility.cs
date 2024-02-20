using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Projectile Shooting Ability", fileName = "ProjectileShootingAbility", order = 4)]
public class ProjectileShootingAbility : PlayerAbility
{
    [SerializeField]
    private FireBall _projectilePrefab;
    private FireBall _currentProjectile;
    [SerializeField]
    private float _existTime;
    [SerializeField]
    private float _moveSpeed;
    public event System.Action<ProjectileShootingAbility> OnProjectileShootingAbilityCoolDown;

    protected override void Activate()
    {
        if (_currentProjectile == null)
        {
            _currentProjectile = GameObject.Instantiate(_projectilePrefab, Instigator.projectileShootingPosition.transform.position, Instigator.transform.rotation);
            _currentProjectile.Setup(_existTime, _moveSpeed);
            OnProjectileShootingAbilityCoolDown?.Invoke(this);
        }
    }
}
