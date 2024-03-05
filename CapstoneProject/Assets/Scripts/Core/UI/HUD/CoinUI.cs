using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private CoinComponent _playerCoinComponent;
    [SerializeField] private TextMeshProUGUI _coinText;
    void Start()
    {
        UpdateCoinText();
        _playerCoinComponent.OnCoinsChanged += UpdateCoinText;
    }

    private void UpdateCoinText()
    {
        string totalCoins = _playerCoinComponent.GetCoins().ToString();
        _coinText.text = totalCoins;
    }
}
