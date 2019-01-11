using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickGenerator : MonoBehaviour {

    public GameObject chick;
    public GameObject sickChick;
    public GameObject bombChick;
    public static ChickGenerator Instance;

    [Header("Magic Chicks")]
    public List<MagicChickData> magicChickDatas;
    public GameObject richPS;
    public GameObject poorPS;

    [Header("Odds")]
    public int sickChickOdds;
    public int bombChickOdds;

    [Header("Bombs")]
    public float bombTimer;

    [Header("Chick Values")]
    public int baseChickValue;
    public int baseSickChickValue;
    public int baseBombChickvalue;
    public int richChickValue;
    public int rickChickSickValue;

    [Header("Chick Pooling")]
    public int chickPoolingAmount;
    public Queue<GameObject> chickPool;
    public Queue<GameObject> sickChickPool;
    public Queue<GameObject> bombChickPool;

    [Header("Chick Eulers")]
    public float minXEul;
    public float maxXEul;
    public float minYEul;
    public float maxYEul;
    public float minZEul;
    public float maxZEul;

    [Header("Chick Chucking")]
    public float chickVelDecayTime;

    [Header("Chick Materials")]
    public Material baseMaterial;

    [Header("FX")]
    public GameObject shellExplosion;
    public GameObject sickShellExplosion;
    public GameObject bombShellExplosion;

    public List<Chick> activeChicks;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //create chick pool and fill it
        chickPool = new Queue<GameObject>();
        sickChickPool = new Queue<GameObject>();
        bombChickPool = new Queue<GameObject>();
        activeChicks = new List<Chick>();
        FillChickPool();
        FillSickChickPool();
        FillBombChickPool();

        foreach(MagicChickData data in magicChickDatas)
        {
            data.chickPool = new Queue<GameObject>();
            for(int i = 0; i < 5; i++)
            {
                newChickGO = Instantiate(data.magicChick, transform);
                data.chickPool.Enqueue(newChickGO);
                newChickGO.SetActive(false);
            }
        }
    }


    GameObject newChickGO;
    Chick newChick;
    public void FillChickPool()
    {
        for (int i = 0; i < chickPoolingAmount; i++)
        {
            newChickGO = Instantiate(chick, transform);
            chickPool.Enqueue(newChickGO);
            newChick = newChickGO.GetComponent<Chick>();
            newChickGO.SetActive(false);
        }
    }
    public void FillSickChickPool()
    {
        for (int i = 0; i < chickPoolingAmount; i++)
        {
            newChickGO = Instantiate(sickChick, transform);
            sickChickPool.Enqueue(newChickGO);
            newChick = newChickGO.GetComponent<Chick>();
            newChickGO.SetActive(false);
        }
    }
    public void FillBombChickPool()
    {
        for (int i = 0; i < chickPoolingAmount; i++)
        {
            newChickGO = Instantiate(bombChick, transform);
            bombChickPool.Enqueue(newChickGO);
            newChick = newChickGO.GetComponent<Chick>();
            newChickGO.SetActive(false);
        }
    }

    Chick spawnedChick;
    GameObject spawnedChickGO;
    Vector3 spawnedChickPos;
    //spawns new chick
    public void SpawnChick(Vector3 _pos)
    {
        //determine chick type
        bool spawnsSickChick = false;
        bool spawnsBombChick = false;
        int pick = Random.Range(0, bombChickOdds);
        if(pick == 0)
        {
            spawnsBombChick = true;
        }
        else
        {
            pick = Random.Range(0, sickChickOdds);
            if (pick == 0)
            {
                spawnsSickChick = true;
            }
        }
        //determine chick position
        spawnedChickPos = _pos;
        //fill pool if necessary
        if(spawnsBombChick)
        {
            if(bombChickPool.Count <= 0)
            {
                FillBombChickPool();
            }
        }
        else if(spawnsSickChick)
        {
            if (sickChickPool.Count <= 0)
            {
                FillSickChickPool();
            }
        }
        else
        {
            if (chickPool.Count <= 0)
            {
                FillChickPool();
            }
        }
        //spawn the chick and set up value
        int value;
        if(spawnsBombChick)
        {
            spawnedChickGO = bombChickPool.Dequeue();
            value = baseBombChickvalue;
            StartCoroutine(ShellExplosion(2, spawnedChickPos));
        }
        else if(spawnsSickChick)
        {
            spawnedChickGO = sickChickPool.Dequeue();
            value = baseSickChickValue;
            StartCoroutine(ShellExplosion(1, spawnedChickPos));
        }
        else
        {
            spawnedChickGO = chickPool.Dequeue();
            value = baseChickValue;
            StartCoroutine(ShellExplosion(0, spawnedChickPos));
        }

        spawnedChickGO.transform.position = spawnedChickPos;
        spawnedChickGO.transform.eulerAngles = ChooseChickEulers();
        spawnedChickGO.SetActive(true);
        //set the chick up
        spawnedChick = spawnedChickGO.GetComponent<Chick>();
        activeChicks.Add(spawnedChick);
        if(spawnedChick.bomb)
        {
            spawnedChick.bombText.transform.rotation = Quaternion.identity;
        }
        spawnedChick.Initialize();
        spawnedChick.canMove = true;
        spawnedChick.ySpeed = MatManager.Instance.matSpeed;

        //set up chick value
        spawnedChick.value = value;
    }

    public void SpawnMagicChick(Vector3 _pos, int index)
    {
        spawnedChickPos = _pos;
        //get magic chick type
        MagicChickData magicChickData = magicChickDatas[index];
        spawnedChickGO = magicChickData.chickPool.Dequeue();

        spawnedChickGO.transform.position = spawnedChickPos;
        spawnedChickGO.transform.eulerAngles = ChooseChickEulers();
        spawnedChickGO.SetActive(true);
        //set the chick up
        spawnedChick = spawnedChickGO.GetComponent<Chick>();
        spawnedChick.magicChickIndex = index;
        activeChicks.Add(spawnedChick);
        spawnedChick.Initialize();
        spawnedChick.canMove = true;
        spawnedChick.ySpeed = MatManager.Instance.matSpeed;

        //set up chick value
        spawnedChick.value = magicChickData.value;
    }

    Vector3 eul;
    //chooses a rotation for eggs
    public Vector3 ChooseChickEulers()
    {
        eul = new Vector3(Random.Range(minXEul, maxXEul), Random.Range(minYEul, maxYEul), Random.Range(minZEul, maxZEul));
        return eul;
    }

    public IEnumerator ShellExplosion(int index, Vector3 pos)
    {
        GameObject currentShellExplosion;
        if(index == 0)
        {
            currentShellExplosion = Instantiate(shellExplosion, new Vector3(pos.x, pos.y, -2), Quaternion.identity, transform);
        }
        else if(index == 1)
        {
            currentShellExplosion = Instantiate(sickShellExplosion, new Vector3(pos.x, pos.y, -2), Quaternion.identity, transform);
        }
        else
        {
            currentShellExplosion = Instantiate(bombShellExplosion, new Vector3(pos.x, pos.y, -2), Quaternion.identity, transform);
        }
        currentShellExplosion.SetActive(true);
        yield return new WaitForSeconds(1);
        currentShellExplosion.SetActive(false);
    }
}
