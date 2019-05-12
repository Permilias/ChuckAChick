using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuButton : MonoBehaviour {

    public void LoadMenu()
    {
        Time.timeScale = 1;
        GameManager.Instance.LoadMenu();
    }
}
