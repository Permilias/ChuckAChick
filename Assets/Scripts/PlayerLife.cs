using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour {

    public static PlayerLife Instance;

    public Transform lifeBarPivot;

    [HideInInspector]
    public float life;
    public float maxLife;

    public float sideEggDamage;
    public float sideEggShakeStrength;
    public float frontEggDamage;
    public float frontEggShakeStrength;
    public float frontBombDamage;
    public float frontBombShakeStrength;

    public float damageShakeDuration;

    private void Awake()
    {
        Instance = this;
    }

    float scaleMultiplier;
    private void Start()
    {
        scaleMultiplier = 1 / maxLife;
        life = maxLife;
    }

    public void LoseLife(float amount, float shakeStrength)
    {
        ScreenShake.Instance.Shake(damageShakeDuration, shakeStrength);
        life -= amount;
        if (life < 0) life = 0;
        float newScale = life * scaleMultiplier;
        lifeBarPivot.localScale = new Vector3(1, newScale, 1);


    }
}
