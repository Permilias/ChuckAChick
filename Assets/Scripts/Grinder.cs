using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinder : MonoBehaviour {

    public Transform grindingPos;
    public float maxY;
    public float grindingSpeed;
    public float grindingYGain;

    public static Grinder Instance;

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
    }


    public void Grind(Egg egg)
    {
        egg.canMove = false;
        egg.colliderGO.SetActive(false);
        StartCoroutine(GrindEgg(egg));
    }

    public void Grind(Chick chick)
    {
        chick.canMove = false;
        chick.colliderGO.SetActive(false);
        StartCoroutine(GrindChick(chick));
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
