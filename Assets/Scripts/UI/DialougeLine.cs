using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class DialougeLine
{

    [SerializeField] public string text;

    [SerializeField] public TMP_FontAsset font;
    [SerializeField] public int fontSize = 10;

    /// <summary>
    /// How many seconds between when each character appears
    /// </summary>
    [SerializeField] public float scrollSpeed = 0.05f;
    [SerializeField] public Color color = Color.black;
    [SerializeField] public AudioClip textScrollSound;
    [SerializeField] public int lettersBetweenSpeechSounds = 1;

    [SerializeField] public Texture speakerImage;
    [SerializeField] public string speakerName;

    [SerializeField] public bool freezePlayerMovement = true;
    [SerializeField] public bool skippable = true;
    [SerializeField] public float timeUntilAutoProceed = 0f;

    [SerializeField] public string dialougeEventTag = "";


    [SerializeField] public CameraGotoPoint cameraGotoPoint;

    [SerializeField] public SoundTrack soundTrack; // the soundtrack that will attempt to be played when this line appears
    

    
}

