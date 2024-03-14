using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyUI : MonoBehaviour
{
    [SerializeField] private KeyItemComponent _playerKeyItemComponent;
    [SerializeField] private TextMeshProUGUI _keyText;
    void Start()
    {
        UpdateKeyText();
        _playerKeyItemComponent.OnKeysChanged += UpdateKeyText;
    }

    private void UpdateKeyText()
    {
        string totalKeys = _playerKeyItemComponent.GetKeys().ToString();
        _keyText.text = totalKeys;
    }
}
