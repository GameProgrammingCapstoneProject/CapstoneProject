using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TimerUI : MonoBehaviour
{
    public float timeRemaining = 0;
    public bool timeIsRunning = true;
    public TMP_Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        timeIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
