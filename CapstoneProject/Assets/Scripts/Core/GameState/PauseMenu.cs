using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       // if (Input.GetKeyDown(KeyCode.Escape))
      //  {
            if (isPaused && !pauseMenu.activeSelf)
            {
                PauseGame();
                Debug.Log("Paused Game-Update Function");
            }
            else if (!isPaused && pauseMenu.activeSelf)
            {
                ResumeGame();
                Debug.Log("Unpaused Game-UpdateFunction");
            }
        //}
        
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale= 0.0f;
       
       
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
       
    }

    public void GoToMainMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit(); //(intentionally) Doesn't work in the editor, only in an acutal build.
    }
}
