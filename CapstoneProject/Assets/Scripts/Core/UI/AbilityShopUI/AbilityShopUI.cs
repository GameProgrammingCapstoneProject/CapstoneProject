using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class AbilityShopUI : MonoBehaviour
{
    [SerializeField] private PlayerAbility[] _allPlayerAbilities;
    [SerializeField] private Transform _abilitySlotParent;
    [SerializeField] private AbilityUI _abilitySlotPrefab;
    [SerializeField] private TextMeshProUGUI _abilityDescription;
    [SerializeField] private TextMeshProUGUI _abilityCost;
    [SerializeField] private TextMeshProUGUI _buyButtonText;
    [SerializeField] private Image _firstAbilitySetupImage;
    [SerializeField] private Image _secondAbilitySetupImage;
    public List<AbilityInformationAndUI> abilityInformation;
    private AbilityInformationAndUI _currentSelectedAbility;
    public AbilityInformationAndUI CurrentSelectedAbility
    {
        get => _currentSelectedAbility;
        set
        {
            _currentSelectedAbility.ui.IsSelected = false;
            _currentSelectedAbility = value;
            _currentSelectedAbility.ui.IsSelected = true;
            _abilityDescription.text = _currentSelectedAbility.information.abilityDescription;
            _abilityCost.text = _currentSelectedAbility.information.GetAbilityCost().ToString();
            UpdateBuyButtonText();
        }
    }
    private void Start()
    {
        abilityInformation = new List<AbilityInformationAndUI>();
        CreateAbilityButton();
        _currentSelectedAbility = abilityInformation[0];
        _currentSelectedAbility.ui.IsSelected = true;
        PlayerAbilityComponent.OnCurrentAbilitiesChanged += UpdateCurrentSetupAbilityUI;
        UpdateBuyButtonText();
        foreach (PlayerAbility ability in _allPlayerAbilities)
        {
            ability.OnAbilityUnlocked += UpdateBuyButtonText;
        }
    }

    private void CreateAbilityButton()
    {
        abilityInformation.Clear();
        if (_abilitySlotParent.childCount > 0)
        {
            for (int i = 0; i < _allPlayerAbilities.Length; i++)
            {
                Destroy(_abilitySlotParent.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < _allPlayerAbilities.Length; i++)
        {
            AbilityUI newSlot = Instantiate(_abilitySlotPrefab, _abilitySlotParent);
            newSlot.SetupAbilitySlot(_allPlayerAbilities[i]);
            abilityInformation.Add(new AbilityInformationAndUI(newSlot, _allPlayerAbilities[i], i));
        }
    }

    private void UpdateCurrentSetupAbilityUI(PlayerAbility ability, int index)
    {
        
        if (index == 0)
        {
            _firstAbilitySetupImage.enabled = true;
            _firstAbilitySetupImage.sprite = ability.abilityIcon;
        }
        else
        {
            _secondAbilitySetupImage.enabled = true;
            _secondAbilitySetupImage.sprite = ability.abilityIcon;
        }
    }

    private void OnDestroy()
    {
       
        PlayerAbilityComponent.OnCurrentAbilitiesChanged -= UpdateCurrentSetupAbilityUI;
        foreach (PlayerAbility ability in _allPlayerAbilities)
        {
            ability.OnAbilityUnlocked -= UpdateBuyButtonText;
        }
    }

    private void UpdateBuyButtonText()
    {
        _buyButtonText.text = CurrentSelectedAbility.information.IsAbilityUnlocked()
            ? "Press 1 or 2 to assign ability to corresponding slot"
            : "Press E to learn";
    }
}
