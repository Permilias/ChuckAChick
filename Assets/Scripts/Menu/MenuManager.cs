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
        DataManager.Instance.Save(false);

        //FactoryLevelButton.Instance.ChangeLevel(0);
        FactoryLevelButton.Instance.ChangeLevel(UpgradesManager.Instance.factoryLevel);
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

    public void LoadLoadingScreen()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.menuPlayButtonPress);
        StartCoroutine("LoadLoadingScreenCoroutine");
    }

    IEnumerator LoadLoadingScreenCoroutine()
    {
        OpeningScreen.Instance.Close();
        AsyncOperation aop = SceneManager.LoadSceneAsync("LoadingScreen");
        aop.allowSceneActivation = false;
        yield return new WaitForSeconds(0.4f);
        aop.allowSceneActivation = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            LoadLoadingScreen();
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            UpgradesManager.Instance.playerMoney += 1000;
        }
    }
}
