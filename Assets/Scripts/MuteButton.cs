using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteButton : MonoBehaviour {

    public static MuteButton Instance;

    public bool muted;

    MenuButton button;

    private void Awake()
    {
        Instance = this;
        button = GetComponent<MenuButton>();
    }

    private void Update()
    {
        if(button.clicked)
        {
            if (muted)
            {
                Unmute();
            }
            else
            {
                Mute();
            }
            button.clicked = false;
        }
    }

    public void Mute()
    {
        muted = true;
        foreach(AudioSource source in FindObjectsOfType<AudioSource>())
        {
            source.enabled = false;
        }
    }

    public void Unmute()
    {
        muted = false;
        foreach (AudioSource source in FindObjectsOfType<AudioSource>())
        {
            source.enabled = true;
        }
    }
}
