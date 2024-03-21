using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CountdownUI : MonoBehaviour
{
    public float timeRemaining = 100;
    public bool timeIsRunning = true;
    public TMP_Text timeText;
    public PlayerHealthComponent playerHealthComponenet;

    // Start is called before the first frame update
    void Start()
    {
        timeIsRunning = true;
        DisplayTime(timeRemaining);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeIsRunning && timeRemaining >= 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
            Debug.Log(timeRemaining.ToString());
        }
        if (timeRemaining <= 0 && timeIsRunning)
        {
            OutOfTime(1, 10);
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    private void OutOfTime(int damageToTake, float timeToAdd)
    {
        Debug.Log("Uh oh, Spaghettios!");

        playerHealthComponenet.TakeBrainDamage(damageToTake);
        if (timeToAdd == 0)
        {
            timeIsRunning = false;
        }
        else
        {
            timeRemaining += timeToAdd;
        }

    }
}
