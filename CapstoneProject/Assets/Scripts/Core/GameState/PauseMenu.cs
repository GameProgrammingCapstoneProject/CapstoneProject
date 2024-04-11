using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionMenu;
    private bool isOpeningOptionsMenu = false;
    public static bool isPaused = false;
    public Slider musicSlider;
    public Slider soundSlider;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        musicSlider.value = SoundManager.Instance.musicMod;
        soundSlider.value = SoundManager.Instance.soundMod;
        OnSoundSliderValueChanged(SoundManager.Instance.soundMod);
        OnMusicSliderValueChanged(SoundManager.Instance.musicMod);
        musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
        soundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
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

    public void PauseGame()
    {
        CursorManager.EnableCursor();
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;

    }

    public void ContinueGame()
    {
        CursorManager.DisableCursor();
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;

        Time.timeScale= 0.0f;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1.0f;  
    }

    public void OpenPauseMenu()
    {
        CursorManager.EnableCursor();
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
    private void OnSoundSliderValueChanged(float value)
    {
        SoundManager.Instance.ChangeSoundVolume(value);
    }
    private void OnMusicSliderValueChanged(float value)
    {
        SoundManager.Instance.ChangeMusicVolume(value);
    }
}
