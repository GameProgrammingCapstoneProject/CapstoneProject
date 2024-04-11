using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundSlider;
    public GameObject mainMenu;
    public GameObject optionMenu;
    public Button continueButton;

    private void Start()
    {
        CursorManager.EnableCursor();
        ChangeContinueButtonState();
        EnableMainMenu();
        DisableOptionMenu();
        musicSlider.value = SoundManager.Instance.musicMod;
        soundSlider.value = SoundManager.Instance.soundMod;
        //OnSoundSliderValueChanged(SoundManager.Instance.soundMod);
        //OnMusicSliderValueChanged(SoundManager.Instance.musicMod);
        musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
        soundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
        
    }

    public void ChangeContinueButtonState()
    {
        if (checkSave())
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
    }

    public void Continue()
    {
        CursorManager.DisableCursor();
        PlayerSaveData data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            Debug.Log("No save data found!");
            //TODO: Add UI for this!
        }
        else if (data!= null)
        {
            Debug.Log(data.playerScene);
            SceneManager.LoadScene(data.playerScene);
        }
        else
        {
            Debug.Log("Data isn't null but it also isn't not null. May God have mercy on our souls.");
            //TODO: delete OS if this happens
        }
        /*
#if UNITY_EDITOR
        SceneManager.LoadScene(EditorBuildSettings.scenes[1].path);
#else
        SceneManager.LoadScene("Level1");
#endif

        //   SceneManager.LoadScene("Final_Prototype");*/
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
    
    public bool checkSave()
    {
        PlayerSaveData data = SaveSystem.LoadPlayer();

        if (data == null)
            return false; //save does not exist
        else
            return true; //data does exist
    }

    private void OnSoundSliderValueChanged(float value)
    {
        SoundManager.Instance.ChangeSoundVolume(value);
    }
    private void OnMusicSliderValueChanged(float value)
    {
        SoundManager.Instance.ChangeMusicVolume(value);
    }

    public void EnableOptionMenu() => optionMenu.SetActive(true);

    public void DisableOptionMenu() => optionMenu.SetActive(false);

    public void EnableMainMenu() => mainMenu.SetActive(true);
    
    public void DisableMainMenu() => mainMenu.SetActive(false);
}
