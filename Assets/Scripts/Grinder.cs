using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Grinder : MonoBehaviour {

    public Transform grindingPos;
    public float maxY;
    public float grindingSpeed;
    public float grindingYGain;

    public TextMeshPro groundChicksText;

    public static Grinder Instance;

    public GameObject bloodFX;
    public Queue<GameObject> bloodFXs;

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
        grindingIncrements = Mathf.RoundToInt(grindingSpeed * 20);
        grindingYIncrement = grindingYGain / grindingIncrements;
        grindingScaleIncrement = Vector3.one / grindingIncrements;
        //groundChicksText.text = "TOTAL GROUND CHICKS : " + GameManager.Instance.totalGroundChicks.ToString();

        bloodFXs = new Queue<GameObject>();
        FillBloodFXPool();
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
            NumberParticlesManager.Instance.SpawnNumberParticle(chick.value * 10, chick.scoreColor, chick.transform.position, 0.9f, 1.2f, true);
        }
        else if(chick.magic)
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.killChick);
            float size = 1.5f * (chick.value / 5);
            size = size > 2.5f ? 2.5f : size;
            NumberParticlesManager.Instance.SpawnNumberParticle(chick.value * 10, chick.scoreColor, chick.transform.position, 0.7f, size, true);
        }
        else
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.killChick);
            float size = 0.5f + (chick.value / 2);
            size = size > 2 ? 2 : size;
            if (!chick.bomb)
            NumberParticlesManager.Instance.SpawnNumberParticle(chick.value * 10, chick.scoreColor, chick.transform.position, 1, size, true);
        }


        //groundChicksText.text = "TOTAL GROUND CHICKS : " + GameManager.Instance.totalGroundChicks.ToString();
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
        Transform eggTransform = egg.transform;
        egg.targetScale = Vector3.zero;
        egg.smoothSpeed = grindingSpeed - 0.1f;
        for(int i = 0; i < grindingIncrements; i++)
        {
            eggTransform.position += new Vector3(0, grindingYIncrement, 0);
            yield return new WaitForSeconds(0.05f);
        }
        eggTransform.localScale = Vector3.zero;
        egg.Remove();
    }

    IEnumerator GrindChick(Chick chick)
    {
        Transform chickTransform = chick.transform;
        chick.targetScale = Vector3.zero;
        chick.smoothSpeed = grindingSpeed - 0.1f;
        for (int i = 0; i < grindingIncrements; i++)
        {
            chickTransform.position += new Vector3(0, grindingYIncrement, 0);
            yield return new WaitForSeconds(0.05f);
        }
        chickTransform.localScale = Vector3.zero;
        
        chick.Remove();
    }
}
