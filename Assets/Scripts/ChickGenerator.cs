using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickGenerator : MonoBehaviour {

    public GameObject chick;
    public GameObject sickChick;
    public static ChickGenerator Instance;

    [Header("Odds")]
    public int sickChickOdds;

    [Header("Chick Attributes")]
    public float chickYSpeed;

    [Header("Chick Pooling")]
    public int chickPoolingAmount;
    public Queue<GameObject> chickPool;
    public Queue<GameObject> sickChickPool;

    [Header("Chick Eulers")]
    public float minXEul;
    public float maxXEul;
    public float minYEul;
    public float maxYEul;
    public float minZEul;
    public float maxZEul;

    [Header("Chick Chucking")]
    public float chickVelDecayTime;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //create chick pool and fill it
        chickPool = new Queue<GameObject>();
        sickChickPool = new Queue<GameObject>();
        FillChickPool();
        FillSickChickPool();
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

    Chick spawnedChick;
    GameObject spawnedChickGO;
    Vector3 spawnedChickPos;
    //spawns new chick
    public void SpawnChick(Vector3 _pos)
    {
        //determine chick type
        bool spawnsSickChick = false;
        int pick = Random.Range(0, sickChickOdds);
        if(pick == 0)
        {
            spawnsSickChick = true;
        }
        //determine chick position
        spawnedChickPos = _pos;
        //fill pool if necessary
        if(spawnsSickChick)
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
        //spawn the chick
        if(spawnsSickChick)
        {
            spawnedChickGO = sickChickPool.Dequeue();
        }
        else
        {
            spawnedChickGO = chickPool.Dequeue();
        }

        spawnedChickGO.transform.position = spawnedChickPos;
        spawnedChickGO.transform.eulerAngles = ChooseChickEulers();
        spawnedChickGO.SetActive(true);
        //set the chick up
        spawnedChick = spawnedChickGO.GetComponent<Chick>();
        spawnedChick.Initialize();
        spawnedChick.canMove = true;
        spawnedChick.ySpeed = chickYSpeed;
    }

    Vector3 eul;
    //chooses a rotation for eggs
    public Vector3 ChooseChickEulers()
    {
        eul = new Vector3(Random.Range(minXEul, maxXEul), Random.Range(minYEul, maxYEul), Random.Range(minZEul, maxZEul));
        return eul;
    }

}
