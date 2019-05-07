using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


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

    public float chuckingTime;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        leftPos = leftTransform.position.x;
        rightPos = rightTransform.position.x;
    }

    public void Chuck(Egg egg, bool left)
    {
        if(!EggGenerator.Instance.canSpawn)
        {
            egg.CheckIfWronglyRemoved();
        }
        egg.canMove = false;
        egg.rb.velocity = Vector3.zero;
        egg.colliderGO.SetActive(false);
        StartCoroutine(ChuckEgg(egg, left));
        PlayerLife.Instance.LoseLife(PlayerLife.Instance.sideEggDamage, PlayerLife.Instance.sideEggShakeStrength);
        SoundManager.Instance.PlaySound(SoundManager.Instance.damage);
        NumberParticlesManager.Instance.SpawnNumberParticle(PlayerLife.Instance.sideEggDamage, PlayerLife.Instance.damageColor, egg.transform.position, 0.8f, 1.5f, false);
    }

    public void Chuck(Chick chick, bool left)
    {
        chick.canMove = false;
        chick.rb.velocity = Vector3.zero;
        chick.colliderGO.SetActive(false);
        StartCoroutine(ChuckChick(chick, left));
        SoundManager.Instance.PlaySound(SoundManager.Instance.piouDragSain);
        if (chick.bomb)
        {
            if(chuckingBombGivesMoney)
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.getMoney);
                GameManager.Instance.AddScore(bombChickChuckingValue);
                NumberParticlesManager.Instance.SpawnNumberParticle(bombChickChuckingValue * 10, ChickGenerator.Instance.bombColor, chick.transform.position, 0.8f, 1.1f, true);
            }
        }
    }

    IEnumerator ChuckEgg(Egg egg, bool left)
    {
        egg.transform.DOScale(Vector3.zero, chuckingTime);
        if (left)
        {
            egg.transform.DOMove(egg.transform.position - new Vector3(chuckingXGain, 0, 0), chuckingTime);
        }
        else
        {
            egg.transform.DOMove(egg.transform.position + new Vector3(chuckingXGain, 0, 0), chuckingTime);
        }
        yield return new WaitForSeconds(chuckingTime);
        egg.Remove();
    }

    IEnumerator ChuckChick(Chick chick, bool left)
    {
        chick.transform.DOScale(Vector3.zero, chuckingTime);
        if (left)
        {
            chick.transform.DOMove(chick.transform.position - new Vector3(chuckingXGain, 0, 0), chuckingTime);
        }
        else
        {
            chick.transform.DOMove(chick.transform.position + new Vector3(chuckingXGain, 0, 0), chuckingTime);
        }
        yield return new WaitForSeconds(chuckingTime);
        chick.Remove();
        chick.Remove();
    }


}
