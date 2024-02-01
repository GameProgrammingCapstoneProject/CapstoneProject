using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;

public class PlayerAbilityComponent : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    public DashAbility DashAbility;
    public ShieldAbility ShieldAbility;
    public BowShootingAbility BowShootingAbility;
    
    public List<PlayerAbility> playerAbilities { get; private set; }

    private void Start()
    {
        DashAbility.AbilityStart(_player);
        playerAbilities = new List<PlayerAbility>
        {
            ShieldAbility,
            BowShootingAbility
        };
        foreach (PlayerAbility ability in playerAbilities)
        {
            ability.AbilityStart(_player);
            ability.Unlock();
        }
    }

    private void Update()
    {
        DashAbility.AbilityUpdate();
        foreach (PlayerAbility ability in playerAbilities)
        {
            ability.AbilityUpdate();
        }
    }

    public void AddAbility(PlayerAbility ability, int slotNumber)
    {
        if (playerAbilities[slotNumber] != null)
            playerAbilities[slotNumber] = ability;
        else
        {
            playerAbilities.Insert(slotNumber, ability);
        }
    }
    public void RemoveAbility(PlayerAbility ability)
    {
        playerAbilities.Remove(ability);
    }
}
