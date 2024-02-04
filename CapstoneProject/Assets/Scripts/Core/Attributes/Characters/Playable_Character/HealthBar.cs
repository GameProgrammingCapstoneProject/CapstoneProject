using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HealthBar : MonoBehaviour
{
    private PlayerHealth playerHealth;

    public void Setup(PlayerHealth playerHealth)
    {
        this.playerHealth = playerHealth;
    }

    private void Start()
    {
        transform.Find("Bar").localScale = new Vector3(playerHealth.GetHealthPercent(), 1, 1);
        
        PlayerHealth.OnHealthChanged += PlayerHealth_OnHealthChanged;
    }

    private void PlayerHealth_OnHealthChanged(object sender, System.EventArgs e)
    {
        transform.Find("Bar").localScale = new Vector3(playerHealth.GetHealthPercent(), 1, 1);
    }
    private void Update()
    {
       // transform.Find("Bar").localScale = new Vector3(playerHealth.GetHealthPercent(), 1, 1);
       
    }
}
