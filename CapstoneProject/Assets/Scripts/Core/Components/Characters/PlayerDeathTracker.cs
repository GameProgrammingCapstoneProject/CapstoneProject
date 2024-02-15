using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathTracker : MonoBehaviour
{
    // Start is called before the first frame update
    int numDeaths = 0;
    PlayerHealth playerHealth;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.health == 0)
        {
            Debug.Log("Player DIEDDDDD!!!!!!!!!!!!!!");
        }
    }
}
