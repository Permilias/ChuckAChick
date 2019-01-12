﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance;

    public Sound eggCrack;
    public Queue<GameObject> receptaclePool;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        receptaclePool = new Queue<GameObject>();
        templateReceptacle = new GameObject();
        templateReceptacle.AddComponent<AudioSource>();
        FillReceptaclePool();
    }

    GameObject templateReceptacle;
    GameObject newReceptacle;
    public void FillReceptaclePool()
    {
        for(int i = 0; i < 50; i++)
        {
            GameObject newReceptacle = Instantiate(templateReceptacle, transform);
            receptaclePool.Enqueue(newReceptacle);
        }
    }

    public void PlaySound(Sound _sound)
    {
        StartCoroutine(PlaySoundCoroutine(_sound));
    }

    public IEnumerator PlaySoundCoroutine(Sound _sound)
    {
        GameObject _receptacle = receptaclePool.Dequeue();
        AudioSource source = _receptacle.GetComponent<AudioSource>();
        source.enabled = true;
        source.clip = _sound.clips[Random.Range(0, _sound.clips.Count)];
        source.volume = Random.Range(_sound.minVolume, _sound.maxVolume);
        source.pitch = Random.Range(_sound.minPitch, _sound.maxPitch);
        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        source.enabled = false;
        receptaclePool.Enqueue(_receptacle);
    }

}