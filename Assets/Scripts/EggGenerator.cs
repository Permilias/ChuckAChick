using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EggGenerator : MonoBehaviour {

    public GameObject egg;
    public static EggGenerator Instance;

    public float eggFrequency;

    [Header("Magic Eggs")]
    public int magicEggPoolingAmount;

    [Header("Magic Eggs Parameters")]
    public float magicEggFrequency;

    [Header("Egg Pooling")]
    public int eggPoolingAmount;
    public Queue<GameObject> eggPool;

    [Header("Egg Placement")]
    public float minX;
    public float maxX;
    public Transform spawningPos;
    public float yPos;

    [Header("Egg Eulers")]
    public float minXEul;
    public float maxXEul;
    public float minYEul;
    public float maxYEul;
    public float minZEul;
    public float maxZEul;

    [Header("Egg Value")]
    public bool breakingGivesMoney;
    public Color breakingMoneyColor;
    public float breakingMoney;
    public bool breakingHeals;
    public float breakingHealing;
    public bool shockwaves;
    public float shockwaveRadius;
    public LayerMask eggLayer;

    public List<Egg> activeEggs;

    int totalMagicEggOdds;
    public bool canSpawn = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //create chick pool and fill it
        yPos = spawningPos.position.y;
        eggPool = new Queue<GameObject>();
        FillEggPool();
        FillMagicEggPools();
        activeEggs = new List<Egg>();

        //calculate magic egg odds
        foreach(MagicChickData data in ChickGenerator.Instance.magicChickDatas)
        {
            totalMagicEggOdds += data.eggOdds;
        }
    }

    float eggTimer;
    float magicEggTimer;
    private void Update()
    {
        if(canSpawn)
        {
            //elapse egg delay, spawn if elapsed
            eggTimer += Time.deltaTime;
            if (eggTimer >= eggFrequency)
            {
                eggTimer = 0;
                SpawnEgg();
            }

            magicEggTimer += Time.deltaTime;
            if (magicEggTimer >= magicEggFrequency)
            {
                magicEggTimer = 0;
                SpawnMagicEgg();
            }
        }
    }
    
    public void Stop()
    {
        canSpawn = false;
    }

    GameObject newEggGO;
    Egg newEgg;
    public void FillEggPool()
    {
        for(int i = 0; i < eggPoolingAmount; i++)
        {
            newEggGO = Instantiate(egg, transform);
            eggPool.Enqueue(newEggGO);
            newEgg = newEggGO.GetComponent<Egg>();
            newEggGO.SetActive(false);
        }
    }

    public void FillMagicEggPools()
    {
        foreach(MagicChickData data in ChickGenerator.Instance.magicChickDatas)
        {
            data.eggPool = new Queue<GameObject>();
            for (int i = 0; i < magicEggPoolingAmount; i++)
            {
                newEggGO = Instantiate(data.magicEgg, transform);
                data.eggPool.Enqueue(newEggGO);
                newEggGO.SetActive(false);
            }
        }
    }

    Egg spawnedEgg;
    GameObject spawnedEggGO;
    float spawnedEggXPos;
    Vector3 spawnedEggPos;
    //spawns new egg
    public void SpawnEgg()
    {
        //determine egg position
        spawnedEggXPos = ChooseX();
        spawnedEggPos = new Vector3(spawnedEggXPos, yPos, 0);
        //fill pool if necessary
        if(eggPool.Count <= 0)
        {
            FillEggPool();
        }
        //spawn the egg
        spawnedEggGO = eggPool.Dequeue();
        spawnedEggGO.transform.position = spawnedEggPos;
        spawnedEggGO.transform.eulerAngles = ChooseEggEulers();
        spawnedEggGO.SetActive(true);
        //set the egg up
        spawnedEgg = spawnedEggGO.GetComponent<Egg>();
        activeEggs.Add(spawnedEgg);
        spawnedEgg.Initialize();
        spawnedEgg.canMove = true;
        spawnedEgg.ySpeed = MatManager.Instance.matSpeed;
    }

    public void SpawnMagicEgg()
    {
        //determine egg position
        spawnedEggXPos = ChooseX();
        spawnedEggPos = new Vector3(spawnedEggXPos, yPos, 0);
        //choose what magic egg to spawn
        int rand = Random.Range(0, (totalMagicEggOdds + 1));
        int index = 0;
        int i = 0;
        MagicChickData chosenData = new MagicChickData();
        foreach(MagicChickData data in ChickGenerator.Instance.magicChickDatas)
        {
            if(rand <= (data.eggOdds + i))
            {
                chosenData = data;
                break;
            }
            else
            {
                index++;
                i += data.eggOdds;
            }
        }
        spawnedEggGO = chosenData.eggPool.Dequeue();
        //spawn the egg
        spawnedEggGO.transform.position = spawnedEggPos;
        spawnedEggGO.transform.eulerAngles = ChooseEggEulers();
        spawnedEggGO.SetActive(true);
        //set the egg up
        spawnedEgg = spawnedEggGO.GetComponent<Egg>();
        activeEggs.Add(spawnedEgg);
        spawnedEgg.magicChickIndex = index;
        spawnedEgg.Initialize();
        spawnedEgg.canMove = true;
        spawnedEgg.ySpeed = MatManager.Instance.matSpeed;
        spawnedEgg.hp = chosenData.eggHp;
        spawnedEgg.sr.sprite = spawnedEgg.hpSprites[chosenData.eggHp - 1];
    }

    public void SpawnSpecificEgg(int index)
    {
        spawnedEggXPos = ChooseX();
        spawnedEggPos = new Vector3(spawnedEggXPos, yPos, 0);
        //choose egg
        if(index > 2)
        {
            spawnedEggGO = ChickGenerator.Instance.magicChickDatas[index - 3].eggPool.Dequeue();
            spawnedEgg = spawnedEggGO.GetComponent<Egg>();
            spawnedEgg.magicChickIndex = index - 3;
            spawnedEgg.hp = 5;

            spawnedEgg.sr.sprite = spawnedEgg.hpSprites[4];
        }
        else
        {
            spawnedEggGO = eggPool.Dequeue();
            spawnedEgg = spawnedEgg = spawnedEggGO.GetComponent<Egg>();
            spawnedEgg.specificEgg = true;
            spawnedEgg.specificIndex = index;
        }

        //spawn the egg
        spawnedEggGO.transform.position = spawnedEggPos;
        spawnedEggGO.transform.eulerAngles = ChooseEggEulers();
        spawnedEggGO.SetActive(true);
        //set the egg up
        activeEggs.Add(spawnedEgg);
        spawnedEgg.Initialize();
        spawnedEgg.canMove = true;
        spawnedEgg.ySpeed = MatManager.Instance.matSpeed;

    }

    //chooses a random x position
    public float ChooseX()
    {
        float xPos = Random.Range(minX, maxX);
        return xPos;
    }

    Vector3 eul;
    //chooses a rotation for eggs
    public Vector3 ChooseEggEulers()
    {
        eul = new Vector3(Random.Range(minXEul, maxXEul), Random.Range(minYEul, maxYEul), Random.Range(minZEul, maxZEul));
        return eul;
    }

}
