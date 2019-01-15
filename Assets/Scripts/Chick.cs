using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chick : MonoBehaviour {

    public float ySpeed;
    public Rigidbody2D rb;
    public GameObject colliderGO;
    public TextMeshPro bombText;
    public bool sick;
    public bool bomb;
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

    public bool canMove;
    public bool eater;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        colliderGO = GetComponentInChildren<Collider2D>().gameObject;
        clickableObject = colliderGO.GetComponent<ClickableObject>();
    }

    public void Initialize(int animIndex)
    {
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

        if(bomb)
        {
            currentTimer = ChickGenerator.Instance.bombTimer;
        }
    }

    public Vector3 targetScale;
    public float smoothSpeed;
    Vector3 reference;
    private void FixedUpdate()
    {
        transform.localScale = Vector3.SmoothDamp(transform.localScale, targetScale, ref reference, smoothSpeed);

        if (canMove)
        {
            Move();
            if (transform.position.y >= Grinder.Instance.maxY && transform.position.x > Chucker.Instance.leftPos && transform.position.x < Chucker.Instance.rightPos)
            {
                Grinder.Instance.Grind(this);
                rb.velocity = Vector3.zero;
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
    private void Update()
    {
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

    public void Move()
    {
        if(clickableObject.clicked)
        {
            if (feathers != null)
                feathers.SetActive(true);
            Vector3 touchPos;
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
            newVel = (touchPos - transform.position) * 5.5f;
            hasBeenClicked = true;
            velDecaying = false;
        }
        else
        {

            if (hasBeenClicked && !velDecaying)
            {
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
    }

    public void Heal()
    {
        sick = false;
        value = ChickGenerator.Instance.baseChickValue;
        anim.SetInteger("AnimIndex", 0);

        SoundManager.Instance.PlaySound(SoundManager.Instance.healedChick);

        if(PS != null)
        {
            Destroy(PS);
            PS = null;
            Enrich();
        }
    }

    GameObject PS;
    public void Enrich()
    {
        if(PS == null)
        {
             value += ChickGenerator.Instance.richChickValue;
             PS = Instantiate(ChickGenerator.Instance.richPS, transform);
        }

    }
}
