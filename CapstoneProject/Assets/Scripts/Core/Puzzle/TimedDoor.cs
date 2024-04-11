using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TimedDoor : MonoBehaviour
{
    private float timeRemaining;
    public bool timeIsRunning = false;
    public TMP_Text timeText;
    public float timeOpen;
    // public PlayerHealthComponent playerHealthComponenet;

    // SGEDFIUYDFIYLGNBUIOCGHEIUODGIUSYGDIUYSBFGIUGHFDUILGDRFSIUYLUYILFSEGV

    private void Awake()
    {
        timeIsRunning = false;
        timeText.enabled = false;
    }

    public void StartTimer()
    {
        timeIsRunning = true;
        timeText.enabled = true;
        // gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = false;
        SpriteRenderer[] All = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in All)
        {
            sr.enabled = false;
        }
        timeRemaining = timeOpen;
        DisplayTime(timeRemaining);
    }

    // Update is called once per frame
    void Update()
    {
    /*    if (Input.GetKeyDown("j"))
        {
             StartTimer();

        } */
        if (timeIsRunning && timeRemaining >= 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
           // Debug.Log(timeRemaining.ToString());
        }
        if (timeRemaining <= 0 && timeIsRunning)
        {
           OutOfTime();
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
    
    private void OutOfTime()
    {
        timeIsRunning= false;
        Debug.Log("Uh oh, Spaghettios!");
        SpriteRenderer[] All = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in All)
        {
            sr.enabled = true;
        }
        GetComponent<Collider2D>().enabled = true;
     
        gameObject.SetActive(true);

        timeText.enabled = false;



    }
}
