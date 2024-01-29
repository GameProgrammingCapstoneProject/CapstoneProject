using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.StateMachine;
using UnityEngine;

public abstract class PlayerAbility : MonoBehaviour
{
    protected Player Owner;
    [SerializeField]
    private float _cooldown;
    [SerializeField]
    private int _baseDamage;
    private bool _isUnlocked = true;

    private float _coolDownTimer;

    public void Update()
    {
        _coolDownTimer -= Time.deltaTime;
    }
    public virtual bool CanUseAbility()
    {
        bool canBeActivated = _coolDownTimer < 0 && IsUnlocked();
        if (canBeActivated == false)
        {
            // cooldown text and sound
        }

        return canBeActivated;
    }
    public virtual void UseAbility()
    {
        Activate();
        _coolDownTimer = GetCooldown();
    }

    protected abstract void Activate();
    public int GetBaseDamage() => _baseDamage;
    public void SetBaseDamage(int baseDamage) => _baseDamage = baseDamage;
    public void SetCooldown(float cooldown) => _cooldown = cooldown;
    public float GetCooldown() => _cooldown;
    public bool IsUnlocked() => _isUnlocked;
    public void Unlock() => _isUnlocked = true;
    public void Lock() => _isUnlocked = false;
}
