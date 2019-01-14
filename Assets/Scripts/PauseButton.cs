using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour {

    public bool pauses;

    public MenuButton otherButton;
    public GameObject pauseMenu;
    MenuButton button;

    private void Start()
    {
        button = GetComponent<MenuButton>();
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        if(pauses)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            otherButton.gameObject.SetActive(true);
            pauseMenu.SetActive(false);
        }
    }

    private void Update()
    {
        if(button.clicked)
        {
            button.clicked = false;
            Pause();
        }
    }
}
