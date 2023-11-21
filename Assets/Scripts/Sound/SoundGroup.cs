using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundGroup
{
    public string name;
    
    
    [Range(0f,3f)]
    public float volumeFactor = 1f;

    [Range(0.1f,3f)]
    public float pitchFactor = 1f;


    /// <summary>
    /// Which level of pitch randomization this soundgroup will play with. 0 = no randomization.
    /// </summary>
    [Range(0f,1f)]
    public float pitchRandomnessLevel = 0;

    public Sound[] sounds;


}
