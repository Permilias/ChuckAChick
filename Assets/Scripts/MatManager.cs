using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatManager : MonoBehaviour {

    public static MatManager Instance;

    [Header("Difficulty")]
    public float baseDifficulty;
    public float difficulty;
    public float maxDifficulty;
    public float difficultyIncreasingDelay;
    public float difficultyIncreasingDelayReduction;
    public float minDifficultyInscreasingDelay;
    public float difficultyIncreasingAmount;

    [Header("Mat Speed")]
    public float matSpeed;
    public float baseSpeed;
    public float speedPlusMax;
    public float speedMinusMax;
    public float maxSpeed;
    public MeshRenderer mr;
    public float matSpeedMultiplier;

    [Header("Egg Spawning Frequency")]
    public float eggMaxSpawningFrequency;
    public float minFrequency;
    float frequency;
    float eggSpawningFrequencyMultiplier;

    float currentTimer;

    public float resetTimeDifficultyMultiplier;
    public float resetDifficultyMax;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        eggSpawningFrequencyMultiplier = eggMaxSpawningFrequency / maxSpeed;
        Reset();
    }

    private void Update()
    {
        if(!stopped)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer >= difficultyIncreasingDelay)
            {
                difficultyIncreasingDelay -= difficultyIncreasingDelayReduction;
                if (difficultyIncreasingDelay < minDifficultyInscreasingDelay)
                {
                    difficultyIncreasingDelay = minDifficultyInscreasingDelay;
                }
                currentTimer = 0;
                difficulty += difficultyIncreasingAmount;
                if (difficulty > maxDifficulty) difficulty = maxDifficulty;
                SetSpeed((baseSpeed + Random.Range(speedMinusMax, speedPlusMax)) * difficulty);
            }
        }
    }

    public void Reset()
    {
        currentTimer = 0;
        float resetDifficulty = baseDifficulty + (Time.timeSinceLevelLoad * resetTimeDifficultyMultiplier);
        if(resetDifficulty > resetDifficultyMax)
        {
            resetDifficulty = resetDifficultyMax;
        }
        difficulty = resetDifficulty;
        SetSpeed(baseSpeed * difficulty);
    }

    bool stopped;
    public void Stop()
    {
        stopped = true;
        SetSpeed(0);
        currentTimer = 0;
    }

    void SetSpeed(float _speed)
    {
        matSpeed = _speed;

        mr.sharedMaterial.SetFloat("Vector1_BF0E8121", _speed * matSpeedMultiplier);

        if (matSpeed >= maxSpeed)
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
        frequency = eggMaxSpawningFrequency - ((matSpeed - baseSpeed) * eggSpawningFrequencyMultiplier);
        if(frequency < minFrequency)
        {
            frequency = minFrequency;
        }
        if(frequency > eggMaxSpawningFrequency)
        {
            frequency = eggMaxSpawningFrequency;
        }
        EggGenerator.Instance.eggFrequency = frequency;
    }
}
