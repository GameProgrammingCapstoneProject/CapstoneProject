using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoulUI : MonoBehaviour
{
    [SerializeField] private SoulComponent _playerSoulComponent;
    [SerializeField] private TextMeshProUGUI _soulText;
    void Start()
    {
        _playerSoulComponent = GameObject.FindWithTag("Player").GetComponent<SoulComponent>();
        UpdateSoulText();
        _playerSoulComponent.OnSoulsChanged += UpdateSoulText;
    }

    private void UpdateSoulText()
    {
        string totalSouls = _playerSoulComponent.GetSouls().ToString();
        _soulText.text = totalSouls;
    }
}
