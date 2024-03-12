using Core.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    //public int health;
    public float[] Playerposition;

    public PlayerSaveData(Player player, Enemy[] enemies) 
    {
    

        Playerposition = new float[3];
        Playerposition[0] = player.transform.position.x;
        Playerposition[1] = player.transform.position.y;
        Playerposition[2] = player.transform.position.z; 
    }
}
