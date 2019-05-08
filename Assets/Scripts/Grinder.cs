using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Grinder : MonoBehaviour {

    public Transform grinderHolePosition;

    public Transform grindingPos;
    public float maxY;
    public float grindingSpeed;
    public float grindingYGain;

    public TextMeshPro groundChicksText;

    public static Grinder Instance;

    public GameObject bloodFX;
    public Queue<GameObject> bloodFXs;

    public float grindingTime;

    public float grindingDelay;

    private void Awake()
    {
        Instance = this;
    }

    public int grindingIncrements;
    public float grindingYIncrement;
    public Vector3 grindingScaleIncrement;
    private void Start()
    {
        maxY = grindingPos.position.y;
        //groundChicksText.text = "TOTAL GROUND CHICKS : " + GameManager.Instance.totalGroundChicks.ToString();

        bloodFXs = new Queue<GameObject>();
        FillBloodFXPool();
    }

    float grindingCount;
    private void Update()
    {
        grindingCount += Time.deltaTime;
        if(grindingCount >= grindingDelay)
        {
            grindingCount = 0;
            GrindAll();
        }
    }

    GameObject newGO;
    public void FillBloodFXPool()
    {
        for(int i = 0; i < 10; i++)
        {
            newGO = Instantiate(bloodFX, transform);
            bloodFXs.Enqueue(newGO);
            newGO.SetActive(false);
        }
    }

    public void GrindAll()
    {
        foreach(Egg egg in FindObjectsOfType<Egg>())
        {
            if(egg.canBeGround)
            {
                Grind(egg);
                egg.canBeGround = false;
            }
        }
        foreach(Chick chick in FindObjectsOfType<Chick>())
        {
            if(chick.canBeGround)
            {
                Grind(chick);
                chick.canBeGround = false;
            }
        }
    }

    public void Grind(Egg egg)
    {
        if (!EggGenerator.Instance.canSpawn)
        {
            egg.CheckIfWronglyRemoved();
        }
        egg.canMove = false;
        egg.colliderGO.SetActive(false);
        StartCoroutine(GrindEgg(egg));
        PlayerLife.Instance.LoseLife(PlayerLife.Instance.frontEggDamage, PlayerLife.Instance.frontEggShakeStrength);
        SoundManager.Instance.PlaySound(SoundManager.Instance.bigDamage);
        NumberParticlesManager.Instance.SpawnNumberParticle(PlayerLife.Instance.frontEggDamage, PlayerLife.Instance.damageColor, egg.transform.position, 1f, 2f, false);
    }

    public void Grind(Chick chick)
    {
        StartCoroutine(BloodFX(chick.transform.position + new Vector3(0, 0, 1)));
        chick.canMove = false;
        chick.colliderGO.SetActive(false);
        
        if (chick.bomb)
        {
            PlayerLife.Instance.LoseLife(PlayerLife.Instance.frontBombDamage, PlayerLife.Instance.frontBombShakeStrength);
            SoundManager.Instance.PlaySound(SoundManager.Instance.bigDamage);
            NumberParticlesManager.Instance.SpawnNumberParticle(PlayerLife.Instance.frontBombDamage, PlayerLife.Instance.damageColor, chick.transform.position, 1f, 1.5f, false);
        }
        StartCoroutine(GrindChick(chick));
        GameManager.Instance.totalGroundChicks++;
        GameManager.Instance.AddScore(chick.value);
        if(chick.sick)
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.killChick);
            NumberParticlesManager.Instance.SpawnNumberParticle(chick.value, chick.scoreColor, chick.transform.position, 0.9f, 1.2f, true);
        }
        else if(chick.magic)
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.killChick);
            float size = 1.5f * (chick.value / 5);
            size = size > 2.5f ? 2.5f : size;
            NumberParticlesManager.Instance.SpawnNumberParticle(chick.value, chick.scoreColor, chick.transform.position, 0.7f, size, true);
        }
        else
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.killChick);
            float size = 0.5f + (chick.value / 2);
            size = size > 2 ? 2 : size;
            if (!chick.bomb)
            NumberParticlesManager.Instance.SpawnNumberParticle(chick.value, chick.scoreColor, chick.transform.position, 1, size, true);
        }
    }

    IEnumerator BloodFX(Vector3 pos)
    {
        if(bloodFXs.Count < 1)
        {
            FillBloodFXPool();
        }
        GameObject chosenFX = bloodFXs.Dequeue();
        chosenFX.transform.position = pos;
        chosenFX.SetActive(true);
        yield return new WaitForSeconds(1);
        bloodFXs.Enqueue(chosenFX);
        chosenFX.SetActive(false);
    }

    IEnumerator GrindEgg(Egg egg)
    {
        egg.transform.DOMove(new Vector3(egg.transform.position.x, grinderHolePosition.position.y, egg.transform.position.z), grindingTime);
        egg.transform.DOScale(Vector3.zero, grindingTime);
        yield return new WaitForSeconds(grindingTime);
        egg.Remove();
    }

    IEnumerator GrindChick(Chick chick)
    {
        chick.transform.DOMove(new Vector3(chick.transform.position.x, grinderHolePosition.position.y, chick.transform.position.z), grindingTime);
        chick.transform.DOScale(Vector3.zero, grindingTime);
        yield return new WaitForSeconds(grindingTime);
        chick.Remove();
    }
}
