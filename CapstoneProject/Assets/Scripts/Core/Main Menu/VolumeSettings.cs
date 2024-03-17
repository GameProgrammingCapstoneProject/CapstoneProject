using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;


    public void ChangeMusicVolume(float musicValue)
    {
        SoundManager.Instance.ChangeMusicVolume(_musicSlider.value);
    }

    public void ChangeSoundVolume()
    {
        SoundManager.Instance.ChangeSoundVolume(_sfxSlider.value);
    }

}
