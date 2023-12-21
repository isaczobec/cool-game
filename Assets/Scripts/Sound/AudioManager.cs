using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private Sound[] sounds;

    private class FadeInformation {

        public Sound sound;
        public float duration;
        public float time;
        public float startVol;
        public float targetVolume;

        public bool fadeIn;

        public bool finnished = false;

        public void FadeSound() {
            
            time += Time.deltaTime;
            sound.audioSource.volume = Mathf.Lerp(startVol,targetVolume,time/duration);

            if (time >= duration) {
                finnished = true;
            }

        }


    }
    private List<FadeInformation> fadeInformations = new List<FadeInformation>();

    [SerializeField] private SoundGroup[] soundGroups;

    void InitializeSound (Sound s) {

        s.audioSource = gameObject.AddComponent<AudioSource>();

            s.audioSource.clip = s.clip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;

            s.audioSource.spatialBlend = s.spatialBlend;
            s.audioSource.rolloffMode = AudioRolloffMode.Linear;
            s.audioSource.dopplerLevel = s.dopplerLevel;
            s.audioSource.loop = s.loop;
            

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

    private void Update() {
        if (fadeInformations.Count != 0) {
            HandleFading();

        }
    }

    public Sound GetSound(string soundName) {
        return Array.Find(sounds, sound => sound.name == soundName);
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

    public void Stop(string name) {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.audioSource.Stop();

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

    public void FadeAudioSource(bool fadeIn, string soundName, float duration, float targetVolume) {
        //StartCoroutine(Fade(fadeIn, soundName, duration, targetVolume));
        Sound s = Array.Find(sounds, sound => sound.name == soundName);


        Debug.Log("soundtofade");
        Debug.Log(s);

        fadeInformations.Add(new FadeInformation{sound = s, duration = duration, targetVolume = targetVolume, startVol = s.audioSource.volume,fadeIn = fadeIn});
    }

    private void HandleFading() {
        foreach (FadeInformation fadeInformation in fadeInformations) {
            fadeInformation.FadeSound();

        }
        for (int i = fadeInformations.Count-1; i >=0; i--)
            {       
                if (fadeInformations[i].finnished == true)
                {

                    if (fadeInformations[i].fadeIn == false) {
                        fadeInformations[i].sound.audioSource.Stop();
                    }

                    fadeInformations.RemoveAt(i);
                }
            }

        
    }


    /*
    public IEnumerator Fade(bool fadeIn, string soundName, float duration, float targetVolume) {


        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        AudioSource audioSource = s.audioSource;

        Debug.Log("audioSource");
        Debug.Log(audioSource);

        

        float time = 0f;
        float startVol = audioSource.volume;
        while (time < duration) {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVol, targetVolume, time/duration);
            yield return null;
        }

        yield break;

    } */

}
