using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    int deathAmount;
    private void Awake()
    {
 

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void Pause(bool isPaused)
    {
        //Pause Function Here
    }

    public void TrackDeathAmount(int deathAmount)
    {
        this.deathAmount = deathAmount;
        //Rest of recent death tracker here
    }

    public int GetDeaths()
    {
        return deathAmount;
    }


}
