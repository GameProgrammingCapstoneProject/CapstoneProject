using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AbilityInformationAndUI
{
    public AbilityUI ui;
    public PlayerAbility information;
    public int index;

    public AbilityInformationAndUI(AbilityUI abilityUI, PlayerAbility playerAbility, int uiIndex)
    {
        ui = abilityUI;
        information = playerAbility;
        index = uiIndex;
    }
}
