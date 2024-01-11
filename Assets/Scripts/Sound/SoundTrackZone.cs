using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicZone : MonoBehaviour, IZone
{

    [SerializeField] private SoundTrack soundTrack;

    [SerializeField] private bool stopWhenLeft = false; // if the music is paused or stopped when the player leaves

    public void PlayerEntered()
    {
        soundTrack.Play();
    }

    public void PlayerLeft()
    {
        if (stopWhenLeft) {
            soundTrack.Stop();
        } else {
            soundTrack.Pause();
        }
            
    }

    
}
