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
    public List<Sprite> hpSprites;

    public float ySpeed;
    public Rigidbody2D rb;
    public GameObject colliderGO;

    ClickableObject clickableObject;

    public bool canMove;

    public bool specificEgg;
    public int specificIndex;

    public GameObject bodySprite;
    public SpriteRenderer sr;

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

    private void Update()
    {
        if (bodySprite != null)
            bodySprite.transform.rotation = new Quaternion(Quaternion.identity.x, Quaternion.identity.y, transform.rotation.z, Quaternion.identity.w);
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
                    ScreenShake.Instance.Shake(0.1f, 0.05f);
                    Break(false);
                }
                else
                {
                    sr.sprite = hpSprites[hp - 1];
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
                NumberParticlesManager.Instance.SpawnNumberParticle(EggGenerator.Instance.breakingMoney, EggGenerator.Instance.breakingMoneyColor, transform.position, 1.7f, 1.2f, true);  
            }

            if (EggGenerator.Instance.breakingHeals)
            {
                PlayerLife.Instance.GainLife(EggGenerator.Instance.breakingHealing);
            }

            if (!magicEgg)
            {
                if(specificEgg)
                {
                    if(TutorialManager.Instance.waiting1)
                    {
                        TutorialManager.Instance.NextTutorial(1);
                    }
                    ChickGenerator.Instance.SpawnSpecificChick(transform.position, specificIndex);
                }
                else
                {
                    ChickGenerator.Instance.SpawnChick(transform.position);
                }

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

    public void CheckIfWronglyRemoved()
    {
        if(TutorialManager.Instance.waiting1)
        {
            TutorialManager.Instance.NextTutorial(1);
        }
        if (TutorialManager.Instance.waiting2)
        {
            TutorialManager.Instance.NextTutorial(1);
        }
        else if (TutorialManager.Instance.waiting3)
        {
            TutorialManager.Instance.NextTutorial(2);
        }
        else if (TutorialManager.Instance.waiting4 && magicEgg)
        {
            TutorialManager.Instance.NextTutorial(3);
        }
        else if (TutorialManager.Instance.waiting5 && magicEgg)
        {
            TutorialManager.Instance.NextTutorial(4);
        }
        else if (TutorialManager.Instance.waiting6 && magicEgg)
        {
            TutorialManager.Instance.NextTutorial(5);
        }
        else if (TutorialManager.Instance.waiting7 && magicEgg)
        {
            TutorialManager.Instance.NextTutorial(6);
        }
        else if (TutorialManager.Instance.waiting8 && magicEgg)
        {
            TutorialManager.Instance.NextTutorial(7);
        }
    }


}
