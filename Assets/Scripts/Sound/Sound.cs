using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{

    public string name;

    public AudioClip clip;

    [Range(0f,3f)]
    public float volume = 1f;
    [Range(0.1f,3f)]
    public float pitch = 1f;

    /// <summary>
    /// Which level of pitch randomization this sound will play with. 0 = no randomization.
    /// </summary>
    [Range(0f,1f)]
    public float pitchRandomnessLevel = 0;

    [Range(0f,1f)]
    public float spatialBlend = 1f;

    [Range(0f,1f)]
    public float dopplerLevel = 0f;


    [SerializeField] public bool loop;



    [HideInInspector]
    public AudioSource audioSource;

}
