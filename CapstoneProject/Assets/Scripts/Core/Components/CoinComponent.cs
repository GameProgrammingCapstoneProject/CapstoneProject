using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinComponent : MonoBehaviour
{
    private int _coins;
    public event System.Action<int> OnCoinsChanged;

    public bool TryToConsumeCoins(int coins)
    {
        if (_coins < coins) return false;
        _coins -= coins;
        OnCoinsChanged?.Invoke(coins);
        return true;
    }

    public void CollectCoins(int coins)
    {
        _coins += coins;
        OnCoinsChanged?.Invoke(coins);
    }

    public int DropCoins()
    {
        int coinsDropped = _coins;
        _coins = 0;
        OnCoinsChanged?.Invoke(coinsDropped);
        return coinsDropped;
    }
    public int GetCoins() => _coins;
}
