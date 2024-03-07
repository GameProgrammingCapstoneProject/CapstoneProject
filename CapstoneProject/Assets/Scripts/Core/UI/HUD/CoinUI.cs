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
        UpdateCoinText(0);
        _playerCoinComponent.OnCoinsChanged += UpdateCoinText;
    }

    private void UpdateCoinText(int collectedCoins)
    {
        int totalCoins = _playerCoinComponent.GetCoins() + collectedCoins;
        _coinText.text = totalCoins.ToString();
    }
}
