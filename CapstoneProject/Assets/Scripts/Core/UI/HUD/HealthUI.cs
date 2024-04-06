using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private PlayerHealthComponent _playerHealthComponent;
    [SerializeField]
    private Transform _healthUIParent;
    [SerializeField]
    private GameObject _heartUIPrefab;

    private List<GameObject> _heartUIList;
    [SerializeField]
    private Sprite _whiteHeartSprite;

    private int _defaultHealth;

    void Start()
    {
        _playerHealthComponent.OnHealthChanged += UpdateHealthUI;
        UpdateDefaultHealthUI();
        UpdateHealthUI();
    }
    

    private void UpdateDefaultHealthUI()
    {
        _defaultHealth = _playerHealthComponent.GetMaxHealthValue();
        for (int i = 0; i < _defaultHealth; i++)
        {
            Instantiate(_heartUIPrefab, _healthUIParent);
        }
    }
    private void UpdateHealthUI()
    {
        if (_healthUIParent.childCount > 0)
        {
            for (int i = 0; i < _healthUIParent.childCount; i++)
            {
                Destroy(_healthUIParent.GetChild(i).gameObject);
            }
        }
        
        for (int i = 0; i < _defaultHealth; i++)
        {
            var heartObject = Instantiate(_heartUIPrefab, _healthUIParent);
            if (i >= _playerHealthComponent.currentHealth)
            {
                heartObject.GetComponent<Image>().sprite = _whiteHeartSprite;
            }
        }
    }
}
