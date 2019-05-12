using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteButton : MonoBehaviour {

    public static MuteButton Instance;

    public bool muted;

    private void Awake()
    {
        Instance = this;
    }

    public void Activate()
    {
            if (muted)
            {
                Unmute();
            }
            else
            {
                Mute();
            }
    }

    public void Mute()
    {
        muted = true;
        foreach(AudioSource source in FindObjectsOfType<AudioSource>())
        {
            source.mute = false;
        }
    }

    public void Unmute()
    {
        muted = false;
        foreach (AudioSource source in FindObjectsOfType<AudioSource>())
        {
            source.mute = true;
        }
    }
}
