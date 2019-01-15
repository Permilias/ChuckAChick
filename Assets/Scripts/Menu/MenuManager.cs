using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public static MenuManager Instance;



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DataManager.Instance.Load(true, false);
    }

    public void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit");
        DataManager.Instance.Save(false);
    }

    public void OnApplicationPause(bool pause)
    {
        Debug.Log("OnApplicationPause");
        //  DataManager.Instance.Load();
        DataManager.Instance.Save(false);
    }

    public void LoadMain()
    {
        StartCoroutine("LoadMainCoroutine");
    }

    IEnumerator LoadMainCoroutine()
    {
        AsyncOperation aop = SceneManager.LoadSceneAsync("Main");
        aop.allowSceneActivation = false;

        yield return new WaitForSeconds(1);
        DataManager.Instance.Save(false);
        yield return new WaitForEndOfFrame();
        aop.allowSceneActivation = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            LoadMain();
        }
    }
}
