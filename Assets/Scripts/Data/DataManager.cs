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
        Instance = this;
        SetPath();
    }

    void SetPath()
    {
        path = Path.Combine(Application.persistentDataPath, "data.json");
    }

    public void Save()
    {
        if(!saved)
        {
            Load();
            saved = true;
        }
        Debug.Log("Save");
        SetData();
        SerializeData();
    }

    void SetData()
    {
        data.totalGroundChicks = GameManager.Instance.totalGroundChicks;
    }

    void SerializeData()
    {
        string dataString = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, dataString);
    }

    public void Load()
    {
        if(File.Exists(path))
        {
            DeserializeData();
        }
        else
        {
            data = new Data();
        }

        ExploitData();
    }

    public void DeserializeData()
    {
        string loadedString = File.ReadAllText(path);
        data = JsonUtility.FromJson<Data>(loadedString);
    }

    public void ExploitData()
    {
        GameManager.Instance.totalGroundChicks = data.totalGroundChicks;
    }


}
