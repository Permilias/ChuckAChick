using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuButton : MonoBehaviour {

    MenuButton button;

    private void Awake()
    {
        button = GetComponent<MenuButton>();
    }

    private void Update()
    {
        if(button.clicked)
        {
            button.clicked = false;
            GameManager.Instance.LoadMenu();
        }
    }
}
