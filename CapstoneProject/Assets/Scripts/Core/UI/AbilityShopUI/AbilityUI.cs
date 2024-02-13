using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private GameObject _selectedFrame;
    
    private bool _isSelected = false;
    public bool IsSelected
    {
        get { return _isSelected; }
        set
        {
            if (_isSelected != value)
            {
                _isSelected = value;
                if (_isSelected)
                {
                    _selectedFrame.SetActive(true);
                }
                else
                {
                    _selectedFrame.SetActive(false);
                }
            }
        }
    }
    public void SetupAbilitySlot(PlayerAbility ability)
    {
        _icon.sprite = ability.abilityIcon;
    }
}
