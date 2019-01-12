using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public List<AudioClip> clips;
    [Range(0, 1)]
    public float minVolume;
    [Range(0, 1)]
    public float maxVolume;
    [Range(0, 2)]
    public float minPitch;
    [Range(0, 2)]
    public float maxPitch;
}
