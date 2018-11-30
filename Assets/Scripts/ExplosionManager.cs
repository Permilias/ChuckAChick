using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour {

    public static ExplosionManager Instance;

    public GameObject explosion;
    public Queue<GameObject> explosionPool;
    public int explosionFillAmount;
    public float explosionRepoolingTime;

    [Header("Shaking")]
    public float explosionShakeDuration;
    public float explosionShakeStrength;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        explosionPool = new Queue<GameObject>();
        FillExplosionPool();
    }

    GameObject newExplosion;
    void FillExplosionPool()
    {
        for(int i = 0; i < explosionFillAmount; i++)
        {
            newExplosion = Instantiate(explosion, transform);
            explosionPool.Enqueue(newExplosion);
            newExplosion.GetComponent<Explosion>().repoolingTime = explosionRepoolingTime;
            newExplosion.SetActive(false);
        }
    }

    GameObject spawnedExplosion;
    public void SpawnExplosion(Vector3 _position)
    {
        if(explosionPool.Count <= 0)
        {
            FillExplosionPool();
        }

        spawnedExplosion = explosionPool.Dequeue();
        spawnedExplosion.transform.position = _position;
        spawnedExplosion.SetActive(true);
        spawnedExplosion.GetComponent<Explosion>().Explode();
        ScreenShake.Instance.Shake(explosionShakeDuration, explosionShakeStrength);
    }
}