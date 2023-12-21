using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrack : MonoBehaviour
{

    [SerializeField] public AudioManager audioManager;
    [SerializeField] public bool dissallowMusicOverlap;
    [SerializeField] public int priority;

    [SerializeField] public float fadeInTime;
    [SerializeField] public float fadeOutTime;

    [SerializeField] public float targetVolume = 0.7f;
    [SerializeField] public string soundName;
    public Sound sound;

    private void Start() {
        sound = audioManager.GetSound(soundName);

        Debug.Log("sound:");
        Debug.Log(sound);
    }

    public void Play() {
        SoundTrackHandler.Instance.TryPlaySoundTrack(this);
    }

    public void Stop() {
        SoundTrackHandler.Instance.StopSoundTrack(this);

    }

}
