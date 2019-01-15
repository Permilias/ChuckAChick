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

    public bool lost;

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
        if(!lost)
        {
            ScreenShake.Instance.Shake(damageShakeDuration, shakeStrength);
            life -= amount;

            float newScale = life * scaleMultiplier;
            lifeBarPivot.localScale = new Vector3(1, newScale, 1);

            if (life <= 0)
            {
                lost = true;
                life = 0;
                GameManager.Instance.EndGame();
            }
            else
            {
                MatManager.Instance.Reset();
            }
        }
    }

    public void GainLife(float amount)
    {
        life += amount;
        if (life > maxLife) life = maxLife;
        float newScale = life * scaleMultiplier;
    }
}
