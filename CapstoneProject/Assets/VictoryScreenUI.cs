using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject VictoryScreenUIHolder;
    [SceneName] public string level1Scene;
    [SceneName] public string mainMenuScene;

    private void Start()
    {
        PlayerHealthComponent.OnDead += ShowUI;
    }

    private void ShowUI()
    {
        VictoryScreenUIHolder.SetActive(true);
    }

    public void Restart()
    {
        string scene = SceneManager.GetActiveScene().name;
        Debug.Log("The scene is: " + scene);
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
