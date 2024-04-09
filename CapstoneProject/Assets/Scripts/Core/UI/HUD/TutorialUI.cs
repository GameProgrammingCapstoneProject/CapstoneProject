using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private GameObject tutorialUIHolder;
    [SerializeField] private string tutorialDescription;

    private void Start()
    {
        tutorialUIHolder.SetActive(false);
        tutorialText.text = tutorialDescription;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            tutorialUIHolder.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            tutorialUIHolder.SetActive(false);
        }
    }
}
