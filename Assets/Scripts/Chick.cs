using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chick : MonoBehaviour {

    public float ySpeed;
    public Rigidbody2D rb;
    public GameObject colliderGO;
    public bool sick;

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

        if (canMove)
        {
            Move();
            if (transform.position.y >= Grinder.Instance.maxY)
            {
                Grinder.Instance.Grind(this);
                rb.velocity = Vector3.zero;
            }
            if(transform.position.x < ChickChucker.Instance.leftPos)
            {
                ChickChucker.Instance.Chuck(this, true);
                rb.velocity = Vector3.zero;
            }
            else if(transform.position.x > ChickChucker.Instance.rightPos)
            {
                ChickChucker.Instance.Chuck(this, false);
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
    }

    public Vector3 newVel;
    Vector3 decayingVel;
    Vector3 decayingAmount;
    bool velDecaying;
    bool hasBeenClicked;
    float decayingTime;
    public void Move()
    {
        if(clickableObject.clicked)
        {
            Vector3 touchPos;
#if UNITY_EDITOR
            touchPos = InputHandler.Instance.MouseWorldPosition();
#elif UNITY_ANDROID
            touchPos = new Vector3(InputHandler.Instance.touchPos.x, InputHandler.Instance.touchPos.y, 0);
#endif
            newVel = (touchPos - transform.position) * 3;
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
    }


    public void Remove()
    {
        ChickGenerator.Instance.chickPool.Enqueue(gameObject);
        gameObject.SetActive(false);
    }
}
