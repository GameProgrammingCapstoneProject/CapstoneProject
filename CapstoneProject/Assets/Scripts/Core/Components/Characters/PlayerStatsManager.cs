using Core.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStatManager : MonoBehaviour
{
    // Start is called before the first frame update
    int numDeaths = 0;
    
    PlayerHealth playerHealth;
    Transform playerTransform;


    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.health == 0)
        {
            Debug.Log("Player DIEDDDDD!!!!!!!!!!!!!!");
            playerHealth.health = 20;
            Debug.Log("Player deaths : " + numDeaths);
            numDeaths++;
            Debug.Log("Player deaths : " + numDeaths);


        }
    }
}
