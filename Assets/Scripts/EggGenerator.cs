using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MagicEggType
{
    nurse
}

public class EggGenerator : MonoBehaviour {

    public GameObject egg;
    public static EggGenerator Instance;

    public float eggFrequency;

    [Header("Magic Eggs")]
    public GameObject nurseEgg;
    public Queue<GameObject> nurseEggPool;
    public int nurseEggOdds;
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

    public List<Egg> activeEggs;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //create chick pool and fill it
        yPos = spawningPos.position.y;
        eggPool = new Queue<GameObject>();
        nurseEggPool = new Queue<GameObject>();
        FillEggPool();
        FillNurseEggPool();
        activeEggs = new List<Egg>();
    }

    float eggTimer;
    float magicEggTimer;
    private void Update()
    {
        //elapse egg delay, spawn if elapsed
        eggTimer += Time.deltaTime;
        if(eggTimer >= eggFrequency)
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

    public void FillNurseEggPool()
    {
        for(int i = 0; i < magicEggPoolingAmount; i++)
        {
            newEggGO = Instantiate(nurseEgg, transform);
            nurseEggPool.Enqueue(newEggGO);
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
        int rand = Random.Range(0, (nurseEggOdds + 1));
        if(rand >= 0 && rand <= nurseEggOdds)
        {
            spawnedEggGO = nurseEggPool.Dequeue();
        }
        //spawn the egg
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
