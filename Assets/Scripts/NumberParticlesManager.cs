using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberParticlesManager : MonoBehaviour {

    public static NumberParticlesManager Instance;

    public Queue<NumberParticle> numberParticles;
    public GameObject numberParticle;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        numberParticles = new Queue<NumberParticle>();
        FillNumberParticlesPool();
    }

    GameObject newGO;
    public void FillNumberParticlesPool()
    {
        for(int i = 0; i < 20; i++)
        {
            newGO = Instantiate(numberParticle, transform);
            numberParticles.Enqueue(newGO.GetComponent<NumberParticle>());
            newGO.SetActive(false);
        }
    }

    public void SpawnNumberParticle(float value, Color color, Vector3 pos, float speed, float size, bool money)
    {
        StartCoroutine(NumberParticleCoroutine(value, color, pos, speed, size, money));
    }

    IEnumerator NumberParticleCoroutine(float value, Color color, Vector3 pos, float speed, float size, bool money)
    {        
        NumberParticle chosenParticle = numberParticles.Dequeue();
        chosenParticle.gameObject.SetActive(true);
        chosenParticle.transform.position = pos + new Vector3(0, 0, -2);
        chosenParticle.Play(value, color, speed, size, money);
        yield return new WaitForSeconds(1 * speed);
        numberParticles.Enqueue(chosenParticle);
        chosenParticle.gameObject.SetActive(false);
    }
}
