using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

/* GUIDE TO USING SOUND MANAGER
   BY JOSHUA MULLER

To use the manager, firstly find the SoundManager prefab object and place it into the scene.
There should be two example sounds attached, Player Jump and Player Death.
If not, add these sounds in if needed.
Then, add any sound you need. This should be simple enough, just hit the + At the bottom and add a sound.
You'll need a name, and a clip. Just drag a clip in and give it an apporiate name. Set Volume and Pitch to 1, or fiddle with the values if you please.
Then, to call the sound, the simplest way is to just add the following line into code that runs whenever you want the sound to happen: 
Object.FindObjectOfType<SoundManager> ().Play("Sound Name");
Of course, replace Sound Name with the name of your sound AS IT APPEARS IN THE NAME SECTION OF THE SOUND MANAGER SCRIPT, NOT THE ACUTAL SOUND'S NAME 
Although, these should be the same in most cases anyway.
An example is in PlayerJumpState, causing the PlayerJump sound to play whenever the player's jump state starts.

If you need any more help, please contact Joshua.
*/
public class SoundManager : MonoBehaviour{

    public Sound[] sounds;
    public float soundMod;
    private void Awake()
    {
        if (soundMod < 0 || soundMod > 1)
            soundMod = 1;
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = (s.volume * soundMod);
            s.source.pitch = s.pitch;
        }
       
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = (s.volume * soundMod);
        s.source.Play();
 
    }

    public void PlayLooped(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.loop = true;
        s.source.volume = (s.volume * soundMod);
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

}
