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
    public int playerSouls;
  //  public List<PlayerAbility> playerAbility;
    public int playerAbilityOne;
    public int playerAbilityTwo;
    public bool[] playerUnlockedAbilities;
    public string playerScene;

    public PlayerSaveData(Player player, int health, int coins, int souls, int abilityone, int abilitytwo, string sceneSaved, bool[] abilitiesUnlocked) 
    {
        playerHealth = health;
        playerCoins = coins;
        playerSouls = souls;
        playerAbilityOne = abilityone;
        playerAbilityTwo = abilitytwo;
        playerScene = sceneSaved;
        playerUnlockedAbilities = abilitiesUnlocked;



        playerPosition = new float[3];

        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
        playerPosition[2] = player.transform.position.z; 
    }

 
}
