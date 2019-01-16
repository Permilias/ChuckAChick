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
    public float baseChickValue;
    public float baseSickChickValue;
    public float baseBombChickvalue;
    public float richChickValue;
    public Color baseColor;
    public Color sickColor;
    public Color bombColor;

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

    [Header("Chick Sprites")]
    public Color enrichedColor;

    [Header("FX")]
    public List<ShellExplosionData> shellExplosions;
    public GameObject healingFX;
    public Queue<GameObject> healingFXs;

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
                newGO = Instantiate(data.magicChick, transform);
                data.chickPool.Enqueue(newGO);
                newGO.SetActive(false);
            }
        }

        foreach (ShellExplosionData data in shellExplosions)
        {
            data.shellPool = new Queue<GameObject>();
        }
        foreach(MagicChickData data in magicChickDatas)
        {
            data.shellExplosionPool = new Queue<GameObject>();
        }
        healingFXs = new Queue<GameObject>();

        FillShellExplosionPools();
        FillMagicShellExplosionPools();
        FillHealingFXPool();
    }

    public void FillShellExplosionPools()
    {
        foreach (ShellExplosionData data in shellExplosions)
        {
            for (int i = 0; i < 10; i++)
            {
                newGO = Instantiate(data.shellExplosion, transform);
                data.shellPool.Enqueue(newGO);
                newGO.SetActive(false);
            }
        }
    }

    public void FillMagicShellExplosionPools()
    {
        foreach(MagicChickData data in magicChickDatas)
        {
            newGO = Instantiate(data.shellExplosion, transform);
            data.shellExplosionPool.Enqueue(newGO);
            newGO.SetActive(false);
        }
    }

    public void FillHealingFXPool()
    {
        for(int i = 0; i < 10; i++)
        {
            newGO = Instantiate(healingFX, transform);
            healingFXs.Enqueue(newGO);
            newGO.SetActive(false);
        }
    }


    GameObject newGO;
    Chick newChick;
    public void FillChickPool()
    {
        for (int i = 0; i < chickPoolingAmount; i++)
        {
            newGO = Instantiate(chick, transform);
            chickPool.Enqueue(newGO);
            newChick = newGO.GetComponent<Chick>();
            newGO.SetActive(false);
        }
    }
    public void FillSickChickPool()
    {
        for (int i = 0; i < chickPoolingAmount; i++)
        {
            newGO = Instantiate(sickChick, transform);
            sickChickPool.Enqueue(newGO);
            newChick = newGO.GetComponent<Chick>();
            newGO.SetActive(false);
        }
    }
    public void FillBombChickPool()
    {
        for (int i = 0; i < chickPoolingAmount; i++)
        {
            newGO = Instantiate(bombChick, transform);
            bombChickPool.Enqueue(newGO);
            newChick = newGO.GetComponent<Chick>();
            newGO.SetActive(false);
        }
    }

    Chick spawnedChick;
    GameObject spawnedChickGO;
    Vector3 spawnedChickPos;
    float value;
    int animIndex;
    Color scoreColor;
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

        if(spawnsBombChick)
        {
            spawnedChickGO = bombChickPool.Dequeue();
            value = baseBombChickvalue;
            StartCoroutine(ShellExplosion(2, spawnedChickPos));
            animIndex = 2;
            scoreColor = bombColor;
        }
        else if(spawnsSickChick)
        {
            spawnedChickGO = sickChickPool.Dequeue();
            value = baseSickChickValue;
            StartCoroutine(ShellExplosion(1, spawnedChickPos));
            animIndex = 1;
            scoreColor = sickColor;
        }
        else
        {
            spawnedChickGO = chickPool.Dequeue();
            value = baseChickValue;
            StartCoroutine(ShellExplosion(0, spawnedChickPos));
            animIndex = 0;
            scoreColor = baseColor;
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
        spawnedChick.scoreColor = scoreColor;
        spawnedChick.Initialize(animIndex);
        spawnedChick.canMove = true;
        spawnedChick.ySpeed = MatManager.Instance.matSpeed;

        //set up chick value
        spawnedChick.value = value;
    }

    public void SpawnSpecificChick(Vector3 _pos, int index)
    {
        //determine chick type
        bool spawnsSickChick = false;
        bool spawnsBombChick = false;
        if (index == 2)
        {
            spawnsBombChick = true;
        }
        else if(index == 1)
        {
            spawnsSickChick = true;
        }
        //determine chick position
        spawnedChickPos = _pos;
        //fill pool if necessary
        if (spawnsBombChick)
        {
            spawnedChickGO = bombChickPool.Dequeue();
            value = baseBombChickvalue;
            StartCoroutine(ShellExplosion(2, spawnedChickPos));
            animIndex = 2;
            scoreColor = bombColor;
        }
        else if (spawnsSickChick)
        {
            spawnedChickGO = sickChickPool.Dequeue();
            value = baseSickChickValue;
            StartCoroutine(ShellExplosion(1, spawnedChickPos));
            animIndex = 1;
            scoreColor = sickColor;
        }
        else
        {
            spawnedChickGO = chickPool.Dequeue();
            value = baseChickValue;
            StartCoroutine(ShellExplosion(0, spawnedChickPos));
            animIndex = 0;
            scoreColor = baseColor;
        }

        spawnedChickGO.transform.position = spawnedChickPos;
        spawnedChickGO.SetActive(true);
        //set the chick up
        spawnedChick = spawnedChickGO.GetComponent<Chick>();
        activeChicks.Add(spawnedChick);
        if (spawnedChick.bomb)
        {
            spawnedChick.bombText.transform.rotation = Quaternion.identity;
        }
        spawnedChick.scoreColor = scoreColor;
        spawnedChick.Initialize(animIndex);
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

        SoundManager.Instance.PlaySound(SoundManager.Instance.magicCracks[index]);

        spawnedChickGO.transform.position = spawnedChickPos;
        spawnedChickGO.transform.eulerAngles = ChooseChickEulers();
        spawnedChickGO.SetActive(true);
        //set the chick up
        spawnedChick = spawnedChickGO.GetComponent<Chick>();
        spawnedChick.magicChickIndex = index;
        activeChicks.Add(spawnedChick);
        spawnedChick.Initialize(3 + index);
        spawnedChick.canMove = true;
        spawnedChick.ySpeed = MatManager.Instance.matSpeed;

        spawnedChick.scoreColor = magicChickDatas[index].scoreColor;

        StartCoroutine(MagicShellExplosion(magicChickDatas[index], spawnedChickPos));

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
        if(shellExplosions[index].shellPool.Count < 1)
        {
            FillShellExplosionPools();
        }
        GameObject currentShellExplosion;
        currentShellExplosion = shellExplosions[index].shellPool.Dequeue();
        currentShellExplosion.transform.position = new Vector3(pos.x, pos.y, -2);
        currentShellExplosion.SetActive(true);
        yield return new WaitForSeconds(1);
        currentShellExplosion.SetActive(false);
        shellExplosions[index].shellPool.Enqueue(currentShellExplosion);
    }

    public IEnumerator MagicShellExplosion(MagicChickData _data, Vector3 pos)
    {
        if (_data.shellExplosionPool.Count < 1)
        {
            FillMagicShellExplosionPools();
        }
        GameObject currentShellExplosion;
        currentShellExplosion = _data.shellExplosionPool.Dequeue();
        currentShellExplosion.transform.position = new Vector3(pos.x, pos.y, -2);
        currentShellExplosion.SetActive(true);
        yield return new WaitForSeconds(1);
        currentShellExplosion.SetActive(false);
        _data.shellExplosionPool.Enqueue(currentShellExplosion);
    }

    public void SpawnHealingFX(Transform _transform)
    {
        StartCoroutine(HealingFX(_transform));  
    }

    IEnumerator HealingFX(Transform _transform)
    {
        GameObject chosenFX = healingFXs.Dequeue();
        chosenFX.SetActive(true);
        chosenFX.transform.position = _transform.position + new Vector3(0, 0, -1);
        chosenFX.transform.rotation = Quaternion.identity;
        chosenFX.transform.parent = _transform;
        yield return new WaitForSeconds(1);
        healingFXs.Enqueue(chosenFX);
        chosenFX.transform.parent = transform;
        chosenFX.SetActive(false);
    }
}
