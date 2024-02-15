using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI; 
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Slider slider;

    public void UpdateHealthBar(int health, int maxHealth)
    {
        slider.value = (float)health / (float)maxHealth;
    }
    void Update()
    {
        transform.rotation= Camera.main.transform.rotation;
    }
}
