using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class MainMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundSlider;

    private void Start()
    {
        OnMusicValueChanged(SoundManager.Instance.musicMod);
        OnSoundValueChanged(SoundManager.Instance.soundMod);
        musicSlider.value = SoundManager.Instance.musicMod;
        soundSlider.value = SoundManager.Instance.soundMod;
        musicSlider.onValueChanged.AddListener(OnMusicValueChanged);
        soundSlider.onValueChanged.AddListener(OnSoundValueChanged);
    }

    public void Continue()
    {
#if UNITY_EDITOR
        SceneManager.LoadScene(EditorBuildSettings.scenes[1].path);
#else
        SceneManager.LoadScene("Vertical_Slice");
#endif

        //   SceneManager.LoadScene("Final_Prototype");
    }

    public void NewGame()
    {
#if UNITY_EDITOR
        SceneManager.LoadScene(EditorBuildSettings.scenes[1].path);
#else
        SceneManager.LoadScene("Vertical_Slice");
#endif
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

    private void OnMusicValueChanged(float value)
    {
        SoundManager.Instance.musicMod = value;
    }
    private void OnSoundValueChanged(float value)
    {
        SoundManager.Instance.soundMod = value;
    }
}
