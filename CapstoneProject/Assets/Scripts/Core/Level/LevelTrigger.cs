using System;
using System.Collections;
using System.Collections.Generic;
using Core.Entity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    [SceneName]
    public string nextScene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
