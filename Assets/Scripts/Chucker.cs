using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chucker : MonoBehaviour {

    public static Chucker Instance;

    public Transform leftTransform;
    public Transform rightTransform;

    public float leftPos;
    public float rightPos;

    public float chuckingSpeed;
    public int chuckingIncrements;
    public float chuckingXIncrements;
    public float chuckingXGain;
    public Vector3 chuckingScaleIncrement;

    public bool chuckingBombGivesMoney;
    public float bombChickChuckingValue;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        leftPos = leftTransform.position.x;
        rightPos = rightTransform.position.x;

        chuckingIncrements = Mathf.RoundToInt(chuckingSpeed * 20);
        chuckingXIncrements = chuckingXGain / chuckingIncrements;
        chuckingScaleIncrement = Vector3.one / chuckingIncrements;
    }

    public void Chuck(Egg egg, bool left)
    {
        egg.canMove = false;
        egg.rb.velocity = Vector3.zero;
        egg.colliderGO.SetActive(false);
        StartCoroutine(ChuckEgg(egg, left));
        PlayerLife.Instance.LoseLife(PlayerLife.Instance.sideEggDamage, PlayerLife.Instance.sideEggShakeStrength);
    }

    public void Chuck(Chick chick, bool left)
    {
        chick.canMove = false;
        chick.rb.velocity = Vector3.zero;
        chick.colliderGO.SetActive(false);
        StartCoroutine(ChuckChick(chick, left));
        if(chick.bomb)
        {
            if(chuckingBombGivesMoney)
            {
                GameManager.Instance.AddScore(bombChickChuckingValue);
            }
        }
    }

    IEnumerator ChuckEgg(Egg egg, bool left)
    {
        Transform eggTransform = egg.transform;
        egg.targetScale = Vector3.zero;
        egg.smoothSpeed = chuckingSpeed - 0.1f;
        if(left)
        {
            for (int i = 0; i < chuckingIncrements; i++)
            {
                eggTransform.position += new Vector3(-chuckingXIncrements, 0, 0);
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            for (int i = 0; i < chuckingIncrements; i++)
            {
                eggTransform.position += new Vector3(chuckingXIncrements, 0, 0);
                yield return new WaitForSeconds(0.05f);
            }
        }

        eggTransform.localScale = Vector3.zero;
        egg.Remove();
    }

    IEnumerator ChuckChick(Chick chick, bool left)
    {
        Transform chickTransform = chick.transform;
        chick.targetScale = Vector3.zero;
        chick.smoothSpeed = chuckingSpeed - 0.1f;
        if (left)
        {
            for (int i = 0; i < chuckingIncrements; i++)
            {
                chickTransform.position += new Vector3(-chuckingXIncrements, 0, 0);
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            for (int i = 0; i < chuckingIncrements; i++)
            {
                chickTransform.position += new Vector3(chuckingXIncrements, 0, 0);
                yield return new WaitForSeconds(0.05f);
            }
        }

        chickTransform.localScale = Vector3.zero;
        chick.Remove();
    }


}
