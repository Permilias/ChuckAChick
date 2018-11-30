using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

    public static ScreenShake Instance;

    public float shakeDuration;
    public float shakeStrength;
    float currentTime;
    bool shaking;

    public void Shake(float _duration, float _strength)
    {
        shaking = true;
        shakeDuration = _duration;
        shakeStrength = _strength;
        currentTime = 0;
    }

    public void Awake()
    {
        Instance = this;
        baseVec = transform.position;
    }

    Vector3 newPos;
    Vector3 baseVec;
    private void FixedUpdate()
    {
        if(shaking)
        {
            newPos = Random.insideUnitCircle * shakeStrength;
            newPos += baseVec;
            transform.position = newPos;
        }
    }

    private void Update()
    {
        if(shaking)
        {
            currentTime += Time.deltaTime;
            if(currentTime > shakeDuration)
            {
                shaking = false;
                currentTime = 0;
                transform.position = baseVec;
            }
        }
    }
}
