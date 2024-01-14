using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrack : MonoBehaviour
{
    [SerializeField] public string soundName;

    [SerializeField] public bool dissallowMusicOverlap;
    [SerializeField] public int priority;

    [SerializeField] public float fadeInTime;
    [SerializeField] public float fadeOutTime;

    [SerializeField] public float targetVolume = 0.7f;
    public Sound sound;

    public bool isPlaying = false;


    

    

    public void Play() {
        SoundTrackHandler.Instance.TryPlaySoundTrack(this);
    }

    public void Stop() {
        SoundTrackHandler.Instance.StopSoundTrack(this);

    }

    public void Pause() {
        SoundTrackHandler.Instance.PauseSoundTrack(this);

    }

}
