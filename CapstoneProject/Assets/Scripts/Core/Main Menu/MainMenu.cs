using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    }

    public void Option()
    {

    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
