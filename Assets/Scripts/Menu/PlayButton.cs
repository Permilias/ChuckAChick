using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour {

    MenuButton button;
    public Animator chickenAnim;
    public Animator playTextAnim;

    private void Awake()
    {
        button = GetComponent<MenuButton>();
    }

    private void Update()
    {
        if(button.clicked)
        {
            button.clicked = false;
            chickenAnim.SetTrigger("Play");
            playTextAnim.SetTrigger("play");
            MenuManager.Instance.LoadLoadingScreen();
        }
    }
}
