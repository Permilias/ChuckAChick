using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int totalGroundChicks;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DataManager.Instance.Load();
    }

    public void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit");
        DataManager.Instance.Save();
    }

    public void OnApplicationPause(bool pause)
    {
        Debug.Log("OnApplicationPause");
      //  DataManager.Instance.Load();
        DataManager.Instance.Save();
    }


}
