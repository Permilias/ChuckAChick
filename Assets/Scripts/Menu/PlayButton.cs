using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour {

    MenuButton button;
    public Animator chickenAnim;

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
            MenuManager.Instance.LoadLoadingScreen();
        }
    }
}
