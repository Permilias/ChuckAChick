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
        newVel = Vector3.zero;
        rb.velocity = Vector3.zero;
        decayingVel = Vector3.zero;
        targetScale = Vector3.one;
        transform.localScale = Vector3.one;
        currentTouchPos = Vector3.zero;
        hasBeenClicked = false;
        velDecaying = false;
        colliderGO.SetActive(true);

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
            if (transform.position.y >= Grinder.Instance.maxY)
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
            bombText.transform.position = transform.position + new Vector3(0, 0, -1);
            bombText.transform.rotation = Quaternion.identity;
            if(canMove)
            {
                currentTimer -= Time.deltaTime;
                bombText.text = Mathf.RoundToInt(currentTimer).ToString();
                if (currentTimer <= 0)
                {
                    Explode();
                }
            }
        }
    }

    public void Explode()
    {
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
            newVel = (touchPos - transform.position) * 5;
            hasBeenClicked = true;
            velDecaying = false;
        }
        else
        {
            if (hasBeenClicked && !velDecaying)
            {
                velDecaying = true;
                decayingTime = ChickGenerator.Instance.chickVelDecayTime * Mathf.Abs(((newVel.x + newVel.y) / 2));
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
                newVel = new Vector3(0, ySpeed, 0);
            }

        }

        rb.velocity = newVel;
        transform.rotation.SetLookRotation(Vector3.up, newVel);
    }


    public void Remove()
    {
        if(sick)
            ChickGenerator.Instance.sickChickPool.Enqueue(gameObject);
        else if(bomb)
            ChickGenerator.Instance.bombChickPool.Enqueue(gameObject);
        else
            ChickGenerator.Instance.chickPool.Enqueue(gameObject);
        ChickGenerator.Instance.activeChicks.Remove(this);
        gameObject.SetActive(false);
    }
}
