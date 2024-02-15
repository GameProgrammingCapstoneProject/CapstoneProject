using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HealthBar : MonoBehaviour
{
    private EnemyHealth enemyHealth;

    public void Setup(EnemyHealth enemyHealth)
    {
        this.enemyHealth = enemyHealth;
    }

    private void Start()
    {
        transform.Find("Bar").localScale = new Vector3(enemyHealth.GetHealthPercent(), 1, 1);
        
        PlayerHealth.OnHealthChanged += PlayerHealth_OnHealthChanged;
    }

    private void PlayerHealth_OnHealthChanged(object sender, System.EventArgs e)
    {
        transform.Find("Bar").localScale = new Vector3(enemyHealth.GetHealthPercent(), 1, 1);
    }
    private void Update()
    {
        transform.Find("Bar").localScale = new Vector3(enemyHealth.GetHealthPercent(), 1, 1);
       
    }
}
