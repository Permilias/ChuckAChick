using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Egg : MonoBehaviour
{
    public bool magicEgg;
    public int magicChickIndex;
    public int hp;
    public MeshRenderer mr;
    public List<Material> hpMaterials;

    public float ySpeed;
    public Rigidbody2D rb;
    public GameObject colliderGO;

    ClickableObject clickableObject;

    public bool canMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        colliderGO = GetComponentInChildren<Collider2D>().gameObject;
        clickableObject = colliderGO.GetComponent<ClickableObject>();
    }

    public void Initialize()
    {
        clickableObject.fingerId = -1;
        targetScale = Vector3.one;
        transform.localScale = Vector3.one;
        colliderGO.SetActive(true);
    }

    public Vector3 targetScale;
    public float smoothSpeed;
    Vector3 reference;
    private void FixedUpdate()
    {
        transform.localScale = Vector3.SmoothDamp(transform.localScale, targetScale, ref reference, smoothSpeed);

        if(clickableObject.clicked)
        {
            if(magicEgg)
            {
                clickableObject.clicked = false;
                hp--;
                if(hp > 0)
                {
                    SoundManager.Instance.PlaySound(SoundManager.Instance.magicTaps[hp - 1]);
                }


                if (hp <= 0)
                {
                    Break(false);
                }
                else
                {
                    mr.material = hpMaterials[hp - 1];
                }
            }
            else
            {
                Break(true);
            }

        }

        if (canMove)
        {
            Move();
            if (transform.position.y >= Grinder.Instance.maxY)
            {
                Grinder.Instance.Grind(this);
                rb.velocity = Vector3.zero;
            }
            if (transform.position.x < Chucker.Instance.leftPos)
            {
                Chucker.Instance.Chuck(this, true);
                rb.velocity = Vector3.zero;
            }
            else if (transform.position.x > Chucker.Instance.rightPos)
            {
                Chucker.Instance.Chuck(this, false);
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    List<Egg> eggsToBreak;
    float dist;
    public void Break(bool canShockwave)
    {
        if (!GameManager.Instance.gameEnded)
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.eggCrack);
            clickableObject.clicked = false;
            InputHandler.Instance.usedFingerIdList.Remove(clickableObject.fingerId);
            clickableObject.fingerId = -1;

            foreach (EaterChick eater in FindObjectsOfType<EaterChick>())
            {
                dist = Vector3.Distance(transform.position, eater.transform.position);
                if (dist <= eater.detectionRadius)
                {
                    eater.Eat(transform.position);
                }
            }



            if (EggGenerator.Instance.breakingGivesMoney)
            {
                GameManager.Instance.AddScore(EggGenerator.Instance.breakingMoney);
            }

            if (EggGenerator.Instance.breakingHeals)
            {
                PlayerLife.Instance.GainLife(EggGenerator.Instance.breakingHealing);
            }

            if (!magicEgg)
            {
                ChickGenerator.Instance.SpawnChick(transform.position);
            }
            else
            {
                ChickGenerator.Instance.SpawnMagicChick(transform.position, magicChickIndex);
            }

            if (EggGenerator.Instance.shockwaves && canShockwave)
            {
                eggsToBreak = new List<Egg>();
                Collider2D[] colArray = Physics2D.OverlapCircleAll(transform.position, EggGenerator.Instance.shockwaveRadius, LayerMask.GetMask("Default"), -1, 1);
                foreach (Collider2D col in colArray)
                {
                    if (col.tag == "Egg")
                    {
                        eggsToBreak.Add(col.GetComponentInParent<Egg>());
                    }
                }

                eggsToBreak.Remove(this);

                foreach (Egg egg in eggsToBreak)
                {
                    egg.Break(false);
                }

            }
            Remove();
        }
    }

    Vector3 newVel;
    public void Move()
    {
        newVel = new Vector3(0, ySpeed, 0);
        rb.velocity = newVel;
    }

    public void Remove()
    {
        if(magicEgg)
        {
            ChickGenerator.Instance.magicChickDatas[magicChickIndex].eggPool.Enqueue(gameObject);
        }
        else
        {
            EggGenerator.Instance.eggPool.Enqueue(gameObject);
        }
        EggGenerator.Instance.activeEggs.Remove(this);
        gameObject.SetActive(false);
    }


}
