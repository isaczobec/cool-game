using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicZone : MonoBehaviour, IZone
{

    [SerializeField] private SoundTrack soundTrack;

    public void PlayerEntered()
    {
        soundTrack.Play();
    }

    public void PlayerLeft()
    {
        soundTrack.Stop();
    }

    
}
