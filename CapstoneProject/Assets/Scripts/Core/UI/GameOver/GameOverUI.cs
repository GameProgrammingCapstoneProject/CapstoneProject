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
        CursorManager.EnableCursor();
        gameOverUIHolder.SetActive(true);
    }

    public void Restart()
    {
        string scene = SceneManager.GetActiveScene().name;
        Debug.Log("The scene is: " + scene);
        SceneManager.LoadScene(scene);
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
