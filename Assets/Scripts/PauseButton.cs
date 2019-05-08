using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour {

    public bool pauses;

    public GameObject otherButton;
    public GameObject pauseMenu;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        if(pauses)
        {
            pauseMenu.SetActive(true);
            //gameObject.SetActive(false);
            //Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            otherButton.SetActive(true);
            pauseMenu.SetActive(false);
        }
    }
}
