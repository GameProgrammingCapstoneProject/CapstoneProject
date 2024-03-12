using Core.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    //public int health;
    public float[] playerPosition;
    public int playerHealth;
    public int playerCoins;
   // public int playerKeys;

    public PlayerSaveData(Player player, int health, int coins) 
    {
        playerHealth = health;
        playerCoins = coins;
       // playerKeys = keys;
        
        playerPosition = new float[3];

        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
        playerPosition[2] = player.transform.position.z; 
    }
}
