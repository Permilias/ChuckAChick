using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {

    private void Start()
    {
        StartCoroutine("LoadMain");
    }

    IEnumerator LoadMain()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync("Main");
        ao.allowSceneActivation = false;
        yield return new WaitForSeconds(0.5f);
        ao.allowSceneActivation = true;
    }
}
