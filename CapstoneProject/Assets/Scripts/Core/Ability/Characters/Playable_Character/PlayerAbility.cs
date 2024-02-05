using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using Core.StateMachine;
using UnityEngine;

public abstract class PlayerAbility : ScriptableObject
{
    protected Player Instigator;
    [SerializeField]
    private float _cooldown;
    [SerializeField]
    private bool _isUnlocked = true;

    private float _coolDownTimer;
    protected PlayerAbilityComponent AbilityHandler;
    public virtual void AbilityStart(Player player, PlayerAbilityComponent handler)
    {
        Instigator = player;
        AbilityHandler = handler;
    }

    public virtual void AbilityUpdate()
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

    public virtual IEnumerator ActivateRoutine()
    {
        yield return 0;
    }
}
