using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using static Cinemachine.DocumentationSortingAttribute;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Continue()
    {
        SceneManager.LoadScene(EditorBuildSettings.scenes[1].path);


   //   SceneManager.LoadScene("Final_Prototype");
    }

    public void NewGame()
    {
        SceneManager.LoadScene(EditorBuildSettings.scenes[1].path);
        DeleteSave();
    }

    public void Option()
    {

    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
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
}
