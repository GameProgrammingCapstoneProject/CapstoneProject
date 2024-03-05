using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemComponent : MonoBehaviour
{
    [SerializeField]
    private int _keys;
    public event System.Action OnKeysChanged;

    public bool TryToUseKeys()
    {
        if (_keys == 0) return false;
        _keys -= 1;
        OnKeysChanged?.Invoke();
        return true;
    }

    public void PickupKey()
    {
        _keys += 1;
        OnKeysChanged?.Invoke();
    }

    public int DropKeys()
    {
        int keysDropped = _keys;
        _keys = 0;
        OnKeysChanged?.Invoke();
        return keysDropped;
    }
    public int GetKeys() => _keys;
}
