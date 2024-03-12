using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Core.Entity;

public static class SaveSystem 
{
    public static void SavePlayer(Player player, int health, int coins)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.STH";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerSaveData data = new PlayerSaveData(player, health, coins);

        formatter.Serialize(stream, data);

        stream.Close();

        Debug.Log("Game Saved!");
    }

    public static PlayerSaveData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.STH";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerSaveData data = formatter.Deserialize(stream) as PlayerSaveData;
            stream.Close();
            Debug.Log("Game Loaded!");
            return data;

            


        }
        else
        {
            Debug.Log("save file not found in" + path);
            return null;
        }
    }
}
