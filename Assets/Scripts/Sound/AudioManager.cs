using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private Sound[] sounds;

    [SerializeField] private SoundGroup[] soundGroups;

    void InitializeSound (Sound s) {

        s.audioSource = gameObject.AddComponent<AudioSource>();

            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;

            s.audioSource.spatialBlend = s.spatialBlend;
            s.audioSource.rolloffMode = AudioRolloffMode.Linear;
            s.audioSource.dopplerLevel = s.dopplerLevel;
            

    }

    // Start is called before the first frame update
    void Awake()
    {

        

        foreach (Sound s in sounds) {
            InitializeSound(s);
        }

        foreach (SoundGroup sg in soundGroups) {
            foreach (Sound s in sg.sounds) {
                InitializeSound(s);
            }
        }

    }

    public void Play(string name, float pitchFactor = 1f, float volumeFactor = 1f, float randomPitchFactor = 0f) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s != null) {

        float finalRandomPitchFactor = randomPitchFactor + s.pitchRandomnessLevel;
        s.audioSource.pitch = s.pitch * pitchFactor * UnityEngine.Random.Range(1 - finalRandomPitchFactor, 1 + finalRandomPitchFactor);
        
        s.audioSource.volume = s.volume * volumeFactor;
        s.audioSource.PlayOneShot(s.clip);
        }
    }

    public void PlayRandom(string soundGroupName, float pitchFactor = 1f, float volumeFactor = 1f, float randomPitchFactor = 0f, float chanceToPlay = 1f) {


            if (UnityEngine.Random.Range(0f,1f) <= chanceToPlay) {

            SoundGroup sg = Array.Find(soundGroups, soundGroup => soundGroup.name == soundGroupName);

            if (sg != null) {
                int randomIndex = UnityEngine.Random.Range((int)0 , sg.sounds.Length);

                Sound s = sg.sounds[randomIndex];

                float finalRandomPitchFactor = randomPitchFactor + s.pitchRandomnessLevel + sg.pitchRandomnessLevel;
                s.audioSource.pitch = s.pitch * sg.pitchFactor * pitchFactor * UnityEngine.Random.Range(1 - randomPitchFactor, 1 + randomPitchFactor);

                s.audioSource.volume = s.volume * sg.volumeFactor * volumeFactor;
                s.audioSource.PlayOneShot(s.clip);
            }

            }

        

       
    }

}
