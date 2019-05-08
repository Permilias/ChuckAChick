using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour {

    public static PlayerLife Instance;

    public List<LifeBulb> lifeBulbs;
    public GameObject lifeBulb;
    public float lifeBulbDistance;
    public Transform lifeBulbParent;

    public int life;
    public int maxLife;

    public int frontEggDamage;
    public float frontEggShakeStrength;
    public int frontBombDamage;
    public float frontBombShakeStrength;

    public float damageShakeDuration;

    public bool lost;

    public Color damageColor;

    public Sprite yellowBulbSprite;
    public Sprite redBulbSprite;
    public Sprite brokenBulbSprite;

    private void Awake()
    {
        Instance = this;
    }

    GameObject newBulb;
    RectTransform newRect;
    LifeBulb newLifeBulb;
    private void Start()
    {
        life = maxLife;

        lifeBulbs = new List<LifeBulb>();

        int i = 0;
        int a = 0;
        for(i = 0; i < life; i+=2)
        {
            newBulb = Instantiate(lifeBulb, lifeBulbParent);
            newRect = newBulb.GetComponent<RectTransform>();
            newLifeBulb = newBulb.GetComponent<LifeBulb>();
            newRect.anchoredPosition = lifeBulbParent.GetComponent<RectTransform>().anchoredPosition + new Vector2(0, lifeBulbDistance * a);
            newRect.anchorMin = new Vector2(0, 1);
            newRect.anchorMax = new Vector2(0, 1);
            newRect.pivot = new Vector2(0, 1);
            lifeBulbs.Add(newLifeBulb);
            a++;
            
            if(i >= maxLife - 2)
            {
                newLifeBulb.underEnd.SetActive(true);
                newLifeBulb.underCables.SetActive(false);
            }
            else
            {
                newLifeBulb.underEnd.SetActive(false);
                newLifeBulb.underCables.SetActive(true);
            }
        }
        foreach (LifeBulb bulb in lifeBulbs)
        {
            i -= 2;
            bulb.lifeIndex = i;
        }

        lifeBulb.SetActive(false);
    }

    public void RefreshAllBulbs()
    {
        foreach (LifeBulb bulb in lifeBulbs)
        {
            bulb.RefreshState();
        }
    }

    public void LoseLife(int amount, float shakeStrength)
    {
        if (!lost)
        {
            ScreenShake.Instance.Shake(damageShakeDuration, shakeStrength);
            life -= amount;

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

        RefreshAllBulbs();
    }

    public void GainLife(int amount)
    {
        life += amount;
        if (life > maxLife) life = maxLife;
        if (life < 0) life = 0;

        RefreshAllBulbs();
    }
}
