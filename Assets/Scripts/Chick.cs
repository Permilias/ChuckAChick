using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Chick : MonoBehaviour {

    public float ySpeed;
    public Rigidbody2D rb;
    public GameObject colliderGO;
    public TextMeshPro bombText;
    public bool sick;
    public bool bomb;
    public GameObject bombAura;
    public float currentTimer;

    public GameObject bodySprite;
    public GameObject feathers;
    public GameObject magicAura;

    public float value;

    public SpriteRenderer SR;

    public Animator anim;

    public int magicChickIndex;
    public bool magic;
    ClickableObject clickableObject;

    public Color scoreColor;

    public bool canMove;
    public bool eater;

    public bool vortexEatsSick;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        colliderGO = GetComponentInChildren<Collider2D>().gameObject;
        clickableObject = colliderGO.GetComponent<ClickableObject>();
    }
    private void Start()
    {
        pulseTime = ChickGenerator.Instance.chickPulseTime;
        pulseSize = ChickGenerator.Instance.chickPulseSize;
        pulseMultiplier = pulseSize / pulseTime;
        baseScale = SR.transform.localScale;
        SR.sortingOrder = Random.Range(900, 1000);
        vortexEatsSick = UpgradesApplier.Instance.vortexEatsSick;
    }

    public void Initialize(int animIndex)
    {
        rich = false;
        canBeGround = false;
        chickCollider.enabled = true;
        eaten = false;
        clickableObject.fingerId = -1;
        clickableObject.clicked = false;
        newVel = Vector3.zero;
        rb.velocity = Vector3.zero;
        decayingVel = Vector3.zero;
        targetScale = Vector3.one;
        transform.localScale = Vector3.one;
        currentTouchPos = Vector3.zero;
        hasBeenClicked = false;
        velDecaying = false;
        colliderGO.SetActive(true);
        anim.SetInteger("AnimIndex", animIndex);

        SR.color = Color.white;

        if(bomb)
        {
            bombAura.SetActive(true);
            currentTimer = ChickGenerator.Instance.bombTimer;
        }

        if(magic)
        {
            if(GetComponent<BreakerChick>() != null)
            {
                if (UpgradesApplier.Instance.breakerHasBonusAura)
                {
                    GetComponent<MagicChickAura>().StartAura(2);
                }
                else
                {
                    GetComponent<MagicChickAura>().StartAura(1);
                }
            }
            else
            {
                GetComponent<MagicChickAura>().StartAura(1);
            }

        }
    }

    public Vector3 targetScale;
    public float smoothSpeed;
    Vector3 reference;
    public bool canBeGround;
    private void FixedUpdate()
    {
        transform.localScale = Vector3.SmoothDamp(transform.localScale, targetScale, ref reference, smoothSpeed);

        if (canMove)
        {
            Move();
            if (transform.position.y >= Grinder.Instance.maxY && transform.position.x > Chucker.Instance.leftPos && transform.position.x < Chucker.Instance.rightPos && !canBeGround)
            {
                canBeGround = true;
            }
            if(transform.position.x < Chucker.Instance.leftPos)
            {
                Chucker.Instance.Chuck(this, true);
                rb.velocity = Vector3.zero;
            }
            else if(transform.position.x > Chucker.Instance.rightPos)
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

    string lastText;
    float dist;
    private void Update()
    {
        UpdatePulse();

        if(velDecaying)
        {
            decayingTime -= Time.deltaTime;
            decayingVel -= decayingAmount * Time.deltaTime;
            if(decayingTime <= 0)
            {
                velDecaying = false;
            }
        }

        if(bomb)
        {
            bombText.transform.position = transform.position + new Vector3(0, 0, -3);
            bombText.transform.rotation = Quaternion.identity;
            if(canMove)
            {
                currentTimer -= Time.deltaTime;
                bombText.text = Mathf.RoundToInt(currentTimer).ToString();
                if(bombText.text != lastText)
                {
                    if(bombText.text == "0")
                    {
                        SoundManager.Instance.PlaySound(SoundManager.Instance.bombLastBip);
                    }
                    else
                    {
                        SoundManager.Instance.PlaySound(SoundManager.Instance.bombBip);
                    }

                }
                lastText = bombText.text;
                if (currentTimer <= 0)
                {
                    Explode();
                }
            }
        }
        if(bodySprite != null)
            bodySprite.transform.rotation = Quaternion.identity;
        if (feathers != null)
            feathers.transform.rotation = Quaternion.identity;
        if (magic && magicAura != null)
        {
            magicAura.transform.position = transform.position + new Vector3(0, 0, 0);
            magicAura.transform.rotation = Quaternion.identity;
        }
        if(PS != null)
        {
            PS.transform.position = transform.position + new Vector3(0, 0, -1);
            PS.transform.rotation = Quaternion.identity;
        }

        if(!bomb && !magic && !eaten)
        {
            foreach (EaterChick eaterScript in ChickGenerator.Instance.activeEaters)
            {
                dist = Vector3.Distance(transform.position, eaterScript.transform.position);
                if (dist <= eaterScript.detectionRadius)
                { 
                    if(sick)
                    {
                        if(vortexEatsSick)
                        {
                            eaten = true;
                            eaterScript.Eat(transform.position, rich);
                            StartCoroutine(GetEaten(eaterScript.transform.position));
                        }
                        
                    }
                    else
                    {
                        eaten = true;
                        eaterScript.Eat(transform.position, rich);
                        StartCoroutine(GetEaten(eaterScript.transform.position));
                    }

                }
            }
        }
    }

    public bool eaten;
    public Collider2D chickCollider;

    public IEnumerator GetEaten(Vector3 targetPos)
    {
        chickCollider.enabled = false;
        transform.DOMove(targetPos, 0.8f);
        transform.DOScale(0, 0.8f);
        yield return new WaitForSeconds(0.8f);
        Remove();
    }

    public void Explode()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.bombExplosion);
        ExplosionManager.Instance.SpawnExplosion(transform.position);
        Remove();
    }

    public Vector3 newVel;
    Vector3 decayingVel;
    Vector3 decayingAmount;
    bool velDecaying;
    bool hasBeenClicked;
    float decayingTime;

    Vector3 currentTouchPos;

    public float pulseTimer;
    float pulseTime;
    float pulseSize;
    float pulseMultiplier;
    float bonusSize;
    Vector3 baseScale;
    bool goingUp = true;
    public void UpdatePulse()
    {
            if(goingUp)
            {
            pulseTimer += Time.deltaTime;
            if (hasBeenClicked)
            {
                pulseTimer += Time.deltaTime;
            }
                if (pulseTimer >= pulseTime)
                {
                    pulseTimer = pulseTime;
                    goingUp = false;
                }
            }
            else
            {
                pulseTimer -= Time.deltaTime;
            if (hasBeenClicked)
            {
                pulseTimer -= Time.deltaTime;
            }
            if (pulseTimer <= 0)
                {
                    pulseTimer = 0;
                    goingUp = true;
                }
            }

            bonusSize = pulseTimer * pulseMultiplier;
            SR.transform.localScale = baseScale + new Vector3(bonusSize, bonusSize, 0);
    }

    public void Move()
    {
        if(clickableObject.clicked)
        {
            if (feathers != null)
                feathers.SetActive(true);
            Vector3 touchPos = Vector3.zero;
#if UNITY_EDITOR
            touchPos = InputHandler.Instance.MouseWorldPosition();
#elif UNITY_ANDROID
            foreach (Touch touch in Input.touches)
            {
                if(touch.fingerId == clickableObject.fingerId)
                {
                    currentTouchPos = InputHandler.Instance.cam.ScreenToWorldPoint(touch.position);
                }
            }
            touchPos = new Vector3(currentTouchPos.x, currentTouchPos.y, 0);
#endif
            newVel = (touchPos - transform.position) * 6.5f;
            hasBeenClicked = true;
            velDecaying = false;
        }
        else
        {

            if (hasBeenClicked && !velDecaying)
            {
                if (sick)
                {
                    SoundManager.Instance.PlaySound(SoundManager.Instance.piouDragMalade);
                }
                if (!sick)
                {
                    SoundManager.Instance.PlaySound(SoundManager.Instance.piouDragSain);
                }
                velDecaying = true;
                decayingTime = ChickGenerator.Instance.chickVelDecayTime * Mathf.Abs(((newVel.x + newVel.y) / 2f));
                decayingVel = newVel;
                decayingAmount = decayingVel / decayingTime;
                hasBeenClicked = false;
            }

            if(velDecaying)
            {
                newVel = decayingVel;
                newVel += new Vector3(0, ySpeed, 0);
            }
            else
            {
                if (feathers != null)
                    feathers.SetActive(false);
                newVel = new Vector3(0, ySpeed, 0);
            }

        }


        rb.velocity = newVel;

    }


    public void Remove()
    {
        Destroy(PS);
        PS = null;
        if(magic)
        {
            if(eater)
            {
                GetComponent<EaterChick>().value = 0;
                GetComponent<EaterChick>().valueText.text = "0";
                ChickGenerator.Instance.activeEaters.Remove(GetComponent<EaterChick>());
            }
            if(magicChickIndex == 0)
            {
                ChickGenerator.Instance.activeHealers.Remove(GetComponent<NurseChick>());
            }
            ChickGenerator.Instance.magicChickDatas[magicChickIndex].chickPool.Enqueue(gameObject);
        }
        else
        {
            if (sick)
                ChickGenerator.Instance.sickChickPool.Enqueue(gameObject);
            else if (bomb)
                ChickGenerator.Instance.bombChickPool.Enqueue(gameObject);
            else
                ChickGenerator.Instance.chickPool.Enqueue(gameObject);
        }
        ChickGenerator.Instance.activeChicks.Remove(this);
        gameObject.SetActive(false);

        if(!EggGenerator.Instance.canSpawn)
        {
            if (TutorialManager.Instance.waiting2)
            {
                TutorialManager.Instance.NextTutorial(2);
            }
            else if (TutorialManager.Instance.waiting3)
            {
                TutorialManager.Instance.NextTutorial(3);
            }
            else if (TutorialManager.Instance.waiting4)
            {
                TutorialManager.Instance.NextTutorial(4);
            }
            else if (TutorialManager.Instance.waiting5 && magic)
            {
                TutorialManager.Instance.NextTutorial(5);
            }
            else if (TutorialManager.Instance.waiting6 && magic)
            {
                TutorialManager.Instance.NextTutorial(6);
            }
            else if (TutorialManager.Instance.waiting7 && magic)
            {
                TutorialManager.Instance.NextTutorial(7);
            }
            else if (TutorialManager.Instance.waiting8 && magic)
            {
                TutorialManager.Instance.NextTutorial(8);
            }
        }

    }

    public void Heal()
    {
        ChickGenerator.Instance.SpawnHealingFX(transform);

        sick = false;
        value = ChickGenerator.Instance.baseChickValue;
        anim.SetInteger("AnimIndex", 0);
        scoreColor = ChickGenerator.Instance.baseColor;

        SoundManager.Instance.PlaySound(SoundManager.Instance.healedChick);
    }

    public void Soothe()
    {
        if(bomb)
        {
            ChickGenerator.Instance.SpawnHealingFX(transform);
            value = ChickGenerator.Instance.baseChickValue;
            anim.SetInteger("AnimIndex", 0);
            scoreColor = ChickGenerator.Instance.baseColor;

            SoundManager.Instance.PlaySound(SoundManager.Instance.healedChick);

            bombAura.SetActive(false);

            bomb = false;
            currentTimer = 0;
            bombText.text = "";
        }

    }

    public GameObject PS;
    public bool rich;
    public void Enrich()
    {
        if(PS == null)
        {
            rich = true;
            anim.SetInteger("AnimIndex", 7);
            SoundManager.Instance.PlaySound(SoundManager.Instance.moneySoundRepeat);
            value += ChickGenerator.Instance.richChickValue;
             PS = Instantiate(ChickGenerator.Instance.richPS, transform);
        }

    }
}
