using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public Animator anim;
    public float repoolingTime;
    float currentTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(currentTime >= 0)
        {
            currentTime += Time.deltaTime;
            if (currentTime > repoolingTime)
            {
                currentTime = -1;
                ExplosionManager.Instance.explosionPool.Enqueue(gameObject);
                gameObject.SetActive(false);
            }
        }
    }

    public void Explode()
    {
        anim.SetTrigger("explode");
        currentTime = 0;
    }
}
