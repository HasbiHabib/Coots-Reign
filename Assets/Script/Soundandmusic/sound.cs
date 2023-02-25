using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class sound 
{
	// script untuk setting sound
    public string name;                                 // nama suara yang akan dimainkan

    public AudioClip clip;                              // clip yang dipilih

    public AudioMixerGroup Output;
    [Range(0f,1f)]
    public float volume;                                 // besar suara
    [Range(1f,5f)]
    public float pitch;                                   // besar pitch
    [HideInInspector]
    public AudioSource source;                           // audio source
    public bool loop;                                    // loop system

    // FindObjectOfType<SoundManager>().Play("walk"); buat adain suara
 

}
