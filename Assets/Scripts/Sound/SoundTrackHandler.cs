using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrackHandler : MonoBehaviour
{

    public static SoundTrackHandler Instance {get; private set;}

    public List<SoundTrack> soundTrackList = new List<SoundTrack>();

    

    private void Awake() {
        Instance = this;
    }
    
    
    public void TryPlaySoundTrack(SoundTrack soundTrack) {

        Debug.Log(soundTrack);


        if (soundTrack.dissallowMusicOverlap) {

            List<SoundTrack> soundTracksToStop = new List<SoundTrack>();

            int highestPrio = 0; 
            foreach (SoundTrack sT in soundTrackList) {

                if (sT != null && sT.dissallowMusicOverlap == true) {
                    if (sT.priority > highestPrio) {
                        highestPrio = sT.priority;
                        if (sT.priority < soundTrack.priority && sT.dissallowMusicOverlap) {
                            soundTracksToStop.Add(sT);
                        }
                    }

                }

            }

            if (soundTrack.priority > highestPrio) {
                foreach (SoundTrack sT in soundTracksToStop) {
                    StopSoundTrack(sT);
                } 
                PlaySoundTrack(soundTrack);
            }

        } else {
            PlaySoundTrack(soundTrack);

        }

    }

    private void PlaySoundTrack(SoundTrack soundTrack) {

        soundTrackList.Add(soundTrack);


        if (!soundTrack.audioManager.GetSound(soundTrack.sound.name).audioSource.isPlaying) {
            soundTrack.sound.volume = 0f;

            soundTrack.audioManager.Play(soundTrack.sound.name);
        }
        soundTrack.audioManager.FadeAudioSource(true,soundTrack.soundName,soundTrack.fadeInTime,soundTrack.targetVolume);
    }

    public void StopSoundTrack(SoundTrack soundTrack) {

        if (soundTrackList.Contains(soundTrack)) {


            soundTrackList.Remove(soundTrack);

            soundTrack.audioManager.FadeAudioSource(false,soundTrack.soundName,soundTrack.fadeOutTime,0f);
        }

    }



    
}
