using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class soundmanager : MonoBehaviour
{
    // script untukmemanage sound game

   public sound[] sounds;                        // suara yang dikeluarkan

    public bool CanOpenSound = false;
    void Awake()
    {
        // memulai suara
        foreach(sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.Output;

        }

        StartCoroutine(Awaked());
    }

    public void Play (string name)
    {
        if (CanOpenSound == true)
        {
            // mencari sumber suara
            sound s = Array.Find(sounds, sounds => sounds.name == name);
            s.source.Play();
        }
    }
    public void Stop (string name){
        sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.Stop();
    }

    IEnumerator Awaked()
    {
        yield return new WaitForSeconds(0.5f);
        CanOpenSound = true;
    }
}