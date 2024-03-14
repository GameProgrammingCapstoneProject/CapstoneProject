using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionMenu;
    private bool isOpeningOptionsMenu = false;
    public static bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        //  if (Input.GetKeyDown(KeyCode.Escape))
        //  { 
        if (isPaused)
        {
            PauseGame();
            if (isOpeningOptionsMenu == false)
            {
                OpenPauseMenu();
                CloseOptionMenu();
            }
            else
            {
                OpenOptionsMenu();
                ClosePauseMenu();
            }
        }
        else
        {
            CloseOptionMenu();
            ClosePauseMenu();
            ResumeGame();
        }
        //  }

    }

    public static void PauseGame()
    {
        Time.timeScale= 0.0f;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1.0f;  
    }

    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
    }
    public void GoToMainMenu()
    {
        ClosePauseMenu();
        ResumeGame();
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
        
    }
    public void ContinueButton()
    {
        ClosePauseMenu();
        ResumeGame();
        isPaused = false;
    }
    public void OpenOptionsMenu()
    {
        isOpeningOptionsMenu = true;
        optionMenu.SetActive(true);
    }
    public void CloseOptionMenu()
    {
        isOpeningOptionsMenu = false;
        optionMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit(); //(intentionally) Doesn't work in the editor, only in an acutal build.
    }
}
