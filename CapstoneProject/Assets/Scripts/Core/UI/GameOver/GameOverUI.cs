using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUIHolder;
    [SceneName] public string level1Scene;
    [SceneName] public string mainMenuScene;
    private void Start()
    {
        PlayerHealthComponent.OnDead += ShowUI;
    }

    private void ShowUI()
    {
        gameOverUIHolder.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(level1Scene);
    }
    
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        PlayerHealthComponent.OnDead -= ShowUI;
    }
}
