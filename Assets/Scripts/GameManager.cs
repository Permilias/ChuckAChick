using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int totalGroundChicks;

    public static GameManager Instance;

    public TextMeshPro moneyText;

    public int score;
    public int money;
    public int baseMoneyMultiplier;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        moneyText.text = "0 $";
        DataManager.Instance.Load(true, true);
        score = 0;
    }

    public void AddScore(int value)
    {
        score += value;
        money = score * baseMoneyMultiplier;
        moneyText.text = money.ToString() + " $";
    }

    public void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit");
        DataManager.Instance.Save(true);
    }

    public void OnApplicationPause(bool pause)
    {
        Debug.Log("OnApplicationPause");
        //  DataManager.Instance.Load();
        DataManager.Instance.Save(true);
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Menu...");
        DataManager.Instance.Save(true);
        SceneManager.LoadScene("Menu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadMenu();
        }
    }
}
