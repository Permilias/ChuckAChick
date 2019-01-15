using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour {

    public static DataManager Instance;

    public string path;

    public Data data = new Data();

    public bool saved;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SetPath();
    }

    void SetPath()
    {
        path = Path.Combine(Application.persistentDataPath, "data.json");
    }

    public void Save(bool game)
    {
        if(!saved)
        {
            Load(true, game);
            saved = true;
        }
        Debug.Log("Save");
        SetData(game);
        SerializeData();
    }

    void SetData(bool game)
    {
        if(game)
        {
            data.totalGroundChicks = GameManager.Instance.totalGroundChicks;
            data.soundMuted = MuteButton.Instance.muted;
            data.playerMoney = GameManager.Instance.playerMoney;
        }
        else
        {
            data.playerMoney = UpgradesManager.Instance.playerMoney;
            data.soundMuted = MuteButton.Instance.muted;
            if(UpgradesManager.Instance.upgradesArray != null)
            {
                data.upgradesArray = UpgradesManager.Instance.upgradesArray;
            }
     
        }

    }

    void SerializeData()
    {
        string dataString = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, dataString);
    }

    public void Load(bool exploits, bool game)
    {
        print("loading");
        if(File.Exists(path))
        {
            DeserializeData();
            print("Deserializing");
        }
        else
        {
            data = new Data();
            print("creating data");
        }

        if(exploits)
        {
            print("exploiting");
            if (game)
            {
                print("game");
                ExploitDataGame();
            }
            else
            {
                print("menu");
                ExploitDataMenu();
            }
        }
    }

    public void DeserializeData()
    {
        string loadedString = File.ReadAllText(path);
        data = JsonUtility.FromJson<Data>(loadedString);
        Debug.Log(data.playerMoney);
    }

    public void ExploitDataGame()
    {
        GameManager.Instance.totalGroundChicks = data.totalGroundChicks;
        GameManager.Instance.playerMoney = data.playerMoney;
        MuteButton.Instance.muted = data.soundMuted;
        UpgradesApplier.Instance.upgradesArray = data.upgradesArray;
    }

    public void ExploitDataMenu()
    {
        MuteButton.Instance.muted = data.soundMuted;
        UpgradesManager.Instance.upgradesArray = data.upgradesArray;
        UpgradesManager.Instance.playerMoney = data.playerMoney;
    }
}
