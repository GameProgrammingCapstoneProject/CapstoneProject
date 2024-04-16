using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject VictoryScreenUIHolder;
    [SceneName] public string level1Scene;
    [SceneName] public string mainMenuScene;

    private void ShowUI()
    {
        VictoryScreenUIHolder.SetActive(true);
    }

    public void NewGame()
    {
        CursorManager.DisableCursor();
#if UNITY_EDITOR
        SceneManager.LoadScene(EditorBuildSettings.scenes[1].path);
#else
        SceneManager.LoadScene("Level1");
#endif
        DeleteSave();
    }

    private string SavePath
    {
        get { return Application.persistentDataPath + "/player.STH"; }
    }
    public void DeleteSave()
    {
        try
        {
            File.Delete(SavePath);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            Debug.Log("No Save game found to delete");
        }
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
