using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoundTrackHandler : MonoBehaviour
{

    public static SoundTrackHandler Instance {get; private set;}

    public List<SoundTrack> soundTrackList = new List<SoundTrack>();
    [SerializeField] private AudioManager audioManager;

    

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

            if (soundTrack.priority >= highestPrio) {
                foreach (SoundTrack sT in soundTracksToStop) {
                    PauseSoundTrack(sT);
                } 
                PlaySoundTrack(soundTrack);
            }

        } else {
            PlaySoundTrack(soundTrack);

        }

    }


    private void PlaySoundTrack(SoundTrack soundTrack) {

        // SoundTrack targetSoundTrack = soundTrackList.Find(s => s.soundName == soundTrack.soundName);

        // if (targetSoundTrack == null) { // only add this soundtrack to the list if it wasnt playing track again if it

        bool containsSoundTrack = false;
        foreach (SoundTrack st in soundTrackList) {

            
            Debug.Log("st");
            Debug.Log(st);

            Debug.Log("sountrack");
            Debug.Log(soundTrack);

            if (st?.soundName == soundTrack.soundName) {
                containsSoundTrack = true;
                break;
            }
        }

        if (containsSoundTrack == false) {
            soundTrackList.Add(soundTrack);
            }
        

        Sound targetSound = audioManager.GetSound(soundTrack.soundName);

        if (!targetSound.audioSource.isPlaying) {
            targetSound.volume = 0f;

            audioManager.Play(soundTrack.soundName,oneShot:false); // the sound is not played as a one shot since it is music
        }
        audioManager.FadeAudioSource(true,soundTrack.soundName,soundTrack.fadeInTime,soundTrack.targetVolume);


    }

    public void StopSoundTrack(SoundTrack soundTrack, bool autoResumeHighestPriority = true) {


        SoundTrack targetSoundTrack = null;
        foreach (SoundTrack st in soundTrackList) {
            if (st?.soundName == soundTrack.soundName) {
                targetSoundTrack = st;
                break;
            }
        }

        if (targetSoundTrack != null) {


            soundTrackList.Remove(targetSoundTrack);

            audioManager.FadeAudioSource(false,targetSoundTrack.soundName,soundTrack.fadeOutTime,0f);

            if (autoResumeHighestPriority) {
                PlaySoundTrackWithHighestPrio();
            }
        }

        

    }


    private void PlaySoundTrackWithHighestPrio() {

        SoundTrack highestPrioSoundtrack = null;
        float highestPrio = 0f;

        foreach (SoundTrack sT in soundTrackList) {
            if (sT != null) {

                if (sT.dissallowMusicOverlap) {
                    if (sT.priority > highestPrio) {
                        highestPrioSoundtrack = sT;
                        highestPrio = sT.priority;
                    }
                }
            }
        }

        if (highestPrioSoundtrack != null && !audioManager.GetSound(highestPrioSoundtrack.soundName).audioSource.isPlaying) {
            PlaySoundTrack(highestPrioSoundtrack);
        }
    }

    public void PauseSoundTrack(SoundTrack soundTrack, bool autoResumeHighestPriority = true) {

        SoundTrack targetSoundTrack = null;
        foreach (SoundTrack st in soundTrackList) {
            if (st?.soundName == soundTrack.soundName) {
                targetSoundTrack = st;
                break;
            }
        }


        if (targetSoundTrack != null) {

        

            audioManager.FadeAudioSource(false,targetSoundTrack.soundName,soundTrack.fadeOutTime,0f,stopCompletely:false);

            if (autoResumeHighestPriority) {
                PlaySoundTrackWithHighestPrio();
            }
        }

    }

    public void StopAllSoundTracks() {
        foreach (SoundTrack soundTrack in soundTrackList) {
            StopSoundTrack(soundTrack);
        }

        soundTrackList = new List<SoundTrack>();
    }

    



    
}
