using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggGenerator : MonoBehaviour {

    public GameObject egg;
    public static EggGenerator Instance;

    [Header("Spawning Parameters")]
    public float eggFrequency;

    [Header("Egg Attributes")]
    public float eggYSpeed;

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
    }

    float eggTimer;
    private void Update()
    {
        //elapse chicken delay, spawn if elapsed
        eggTimer += Time.deltaTime;
        if(eggTimer >= eggFrequency)
        {
            eggTimer = 0;
            SpawnEgg();
        }
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
        spawnedEgg.Initialize();
        spawnedEgg.canMove = true;
        spawnedEgg.ySpeed = eggYSpeed;
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
