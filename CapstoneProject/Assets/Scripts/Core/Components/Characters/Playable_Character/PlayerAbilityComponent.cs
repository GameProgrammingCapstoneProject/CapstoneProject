using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

public class PlayerAbilityComponent : MonoBehaviour
{
    public DashAbility DashAbility { get; private set; }
    public ShieldAbility ShieldAbility { get; private set; }

    public List<PlayerAbility> playerAbilities;
    private void Awake()
    {
        DashAbility = GetComponent<DashAbility>();
        ShieldAbility = GetComponent<ShieldAbility>();
    }
}
