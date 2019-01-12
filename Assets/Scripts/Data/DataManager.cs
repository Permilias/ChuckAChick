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
            Load(false, true);
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
        }
        else
        {
            data.soundMuted = MuteButton.Instance.muted;
            if(UpgradesManager.Instance.upgradesArray != null)
            data.upgradesArray = UpgradesManager.Instance.upgradesArray;
            data.money = UpgradesManager.Instance.money;
        }

    }

    void SerializeData()
    {
        string dataString = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, dataString);
    }

    public void Load(bool exploits, bool game)
    {
        if(File.Exists(path))
        {
            DeserializeData();
        }
        else
        {
            data = new Data();
        }

        if(exploits)
        {
            if (game)
            {
                ExploitDataGame();
            }
            else
            {
                ExploitDataMenu();
            }
        }
    }

    public void DeserializeData()
    {
        string loadedString = File.ReadAllText(path);
        data = JsonUtility.FromJson<Data>(loadedString);
    }

    public void ExploitDataGame()
    {
        GameManager.Instance.totalGroundChicks = data.totalGroundChicks;
        MuteButton.Instance.muted = data.soundMuted;
        UpgradesApplier.Instance.upgradesArray = data.upgradesArray;
    }

    public void ExploitDataMenu()
    {
        MuteButton.Instance.muted = data.soundMuted;
        if(data.upgradesArray.Length > 0)
        {
            UpgradesManager.Instance.upgradesArray = data.upgradesArray;
        }
        UpgradesManager.Instance.money = data.money;
    }
}
