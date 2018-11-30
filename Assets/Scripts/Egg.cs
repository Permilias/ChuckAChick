using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Egg : MonoBehaviour
{


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
            Break();
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

    public void Break()
    {
        clickableObject.clicked = false;
        ChickGenerator.Instance.SpawnChick(transform.position);
        Remove();
    }

    Vector3 newVel;
    public void Move()
    {
        newVel = new Vector3(0, ySpeed, 0);
        rb.velocity = newVel;
    }

    public void Remove()
    {
        EggGenerator.Instance.eggPool.Enqueue(gameObject);
        gameObject.SetActive(false);
    }


}
