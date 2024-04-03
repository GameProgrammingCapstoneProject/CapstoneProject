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

    private void Start()
    {
        EnableMainMenu();
        DisableOptionMenu();
        musicSlider.value = SoundManager.Instance.musicMod;
        soundSlider.value = SoundManager.Instance.soundMod;
        //OnSoundSliderValueChanged(SoundManager.Instance.soundMod);
        //OnMusicSliderValueChanged(SoundManager.Instance.musicMod);
        musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
        soundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
        
    }

    public void Continue()
    {

  
        SceneManager.LoadScene("Level1");


        //   SceneManager.LoadScene("Final_Prototype");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Level1");

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
