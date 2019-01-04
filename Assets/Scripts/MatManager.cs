using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatManager : MonoBehaviour {

    public static MatManager Instance;

    [Header("Difficulty")]
    public float difficulty;
    public float maxDifficulty;
    public float difficultyIncreasingDelay;
    public float difficultyIncreasingAmount;

    [Header("Mat Speed")]
    public float matSpeed;
    public float baseSpeed;
    public float speedPlusMax;
    public float speedMinusMax;
    public float maxSpeed;

    [Header("Egg Spawning Frequency")]
    public float eggMaxSpawningFrequency;
    public float minFrequency;
    float frequency;
    float eggSpawningFrequencyMultiplier;

    float currentTimer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        eggSpawningFrequencyMultiplier = eggMaxSpawningFrequency / maxSpeed;
    }

    private void Update()
    {
        currentTimer += Time.deltaTime;
        if(currentTimer >= difficultyIncreasingDelay)
        {
            currentTimer = 0;
            difficulty += difficultyIncreasingAmount;
            if (difficulty > maxDifficulty) difficulty = maxDifficulty;
            SetSpeed((baseSpeed + Random.Range(speedMinusMax, speedPlusMax)) * difficulty);
        }
    }

    void SetSpeed(float _speed)
    {
        matSpeed = _speed;
        if(matSpeed >= maxSpeed)
        {
            matSpeed = maxSpeed;
        }
        foreach(Egg egg in EggGenerator.Instance.activeEggs)
        {
            egg.ySpeed = matSpeed;
        }
        foreach(Chick chick in ChickGenerator.Instance.activeChicks)
        {
            chick.ySpeed = matSpeed;
        }
        frequency = eggMaxSpawningFrequency - (matSpeed * eggSpawningFrequencyMultiplier);
        if(frequency < minFrequency)
        {
            frequency = minFrequency;
        }
        EggGenerator.Instance.eggFrequency = frequency;
    }
}
