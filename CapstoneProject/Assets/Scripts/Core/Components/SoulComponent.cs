using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoulComponent : MonoBehaviour
{
    [SerializeField]
    private int _souls;
    public event System.Action OnSoulsChanged;

    public bool TryToConsumeSouls(int souls)
    {
        if (_souls < souls) return false;
        _souls -= souls;
        OnSoulsChanged?.Invoke();
        return true;
    }

    public void CollectSouls(int souls)
    {
        _souls += souls;
        OnSoulsChanged?.Invoke();
    }

    public int DropSouls()
    {
        int soulsDropped = _souls;
        _souls = 0;
        OnSoulsChanged?.Invoke();
        return soulsDropped;
    }
    public int GetSouls() => _souls;

    public void ChangeSouls(int souls)
    {
        _souls = souls;
        OnSoulsChanged?.Invoke();
    }
}
