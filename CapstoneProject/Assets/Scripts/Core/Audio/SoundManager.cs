using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using Core.Extension;

/* GUIDE TO USING SOUND MANAGER
   BY JOSHUA MULLER

To use the manager, firstly find the SoundManager prefab object and place it into the scene, if it isn't already placed.
Then, add any sound you need. This should be simple enough, just hit the + At the bottom and add a sound.
You'll need a name, and a clip. Just drag a clip in and give it an apporiate name. Set Volume and Pitch to 1, or fiddle with the values if you please. 
Volume should stay at 1 unless absolutely necessary, but it's good to fiddle with pitch to make sounds sound different or better.
Then, to call the sound, the simplest way is to just add the following line into code that runs whenever you want the sound to happen: 
Object.FindObjectOfType<SoundManager> ().Play("Sound Name");
Of course, replace Sound Name with the name of your sound AS IT APPEARS IN THE NAME SECTION OF THE SOUND MANAGER SCRIPT, NOT THE ACUTAL SOUND'S NAME 
Although, these should be the same in most cases anyway.
An example is in PlayerJumpState, causing the PlayerJump sound to play whenever the player's jump state starts.
if you want to play a looped sound NOT MUSIC, use
Object.FindObjectOfType<SoundManager> ().PlayLooped("Sound Name");
if you want to stop a sound from playing instantly, use
Object.FindObjectOfType<SoundManager> ().Stop("Sound Name");
if you want to stop a sound/music from looping but let it finish playing one last time, use
Object.FindObjectOfType<SoundManager> ().StopLooping("Sound Name");
Finally, if you want to play music that doesn't loop, (important to make sure adjusting music volumes in settings), use
Object.FindObjectOfType<SoundManager> ().PlayMusic("Sound Name");
if you want to play music that loops, use
Object.FindObjectOfType<SoundManager> ().PlayLoopedMusic("Sound Name");


If you need any more help, please contact Joshua.
*/
public class SoundManager : PersistentObject<SoundManager>
{

    public SoundList SoundList;
    public Sound[] sounds;
    public float soundMod = 1f;
    public float musicMod = 1f;

    protected override void Awake()
    {
        base.Awake();
        
    }

    protected override void Start()
    {
        base.Start();
        SoundList = FindObjectOfType<SoundList>();
        sounds = SoundList.sounds;
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
        Stop("ExampleSong");
        PlayLooped("ExampleSong");
    }

    private void OnDestroy()
    {
        Stop("ExampleSong");
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = s.volume * soundMod;
        s.source.Play();
    }

    public void PlayLooped(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.loop = true;
        s.source.volume = s.volume * soundMod;
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.loop = true;
        s.source.Stop();
    }

    public void StopLooping(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.loop = false;
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = (s.volume * musicMod);
        s.source.loop = false;
    }

    public void PlayLoopedMusic(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = (s.volume * musicMod);
        s.source.loop = false;
    }

    public void ChangeSoundVolume(float soundValue)
    {
        soundMod = soundValue;
    }

    public void ChangeMusicVolume(float musicValue)
    {
        musicMod = musicValue;
    }

}
