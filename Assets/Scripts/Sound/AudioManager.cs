using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds) {
            s.audioSource = gameObject.AddComponent<AudioSource>();

            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
        }
    }

    public void Play(string name, float pitch = 1f) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        s.audioSource.pitch = pitch;
        s.audioSource.PlayOneShot(s.clip);
    }

}
