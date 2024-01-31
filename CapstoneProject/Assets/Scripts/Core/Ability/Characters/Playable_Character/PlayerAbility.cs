using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.StateMachine;
using UnityEngine;

public abstract class PlayerAbility
{
    protected Player Instigator;
    private float _cooldown;
    private bool _isUnlocked = true;

    private float _coolDownTimer;

    public PlayerAbility(Player player, float cooldown)
    {
        Instigator = player;
        SetCooldown(cooldown);
    }

    public virtual void Update()
    {
        _coolDownTimer -= Time.deltaTime;
    }
    public virtual bool CanUseAbility()
    {
        bool canBeActivated = _coolDownTimer < 0 && IsAbilityUnlocked();
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

    public void SetCooldown(float cooldown) => _cooldown = cooldown;
    public float GetCooldown() => _cooldown;
    public bool IsAbilityUnlocked() => _isUnlocked;
    public void Unlock() => _isUnlocked = true;
    public void Lock() => _isUnlocked = false;
}
