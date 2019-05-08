using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBulb : MonoBehaviour {

    public Image bulbImage;
    public int lifeIndex;

    public GameObject underCables;
    public GameObject underEnd;

    int playerLife;
    public void RefreshState()
    {
        playerLife = PlayerLife.Instance.life;
        if(playerLife >= lifeIndex+2)
        {
            bulbImage.sprite = PlayerLife.Instance.yellowBulbSprite;
        }
        else if(playerLife == lifeIndex+1)
        {
            bulbImage.sprite = PlayerLife.Instance.redBulbSprite;
        }
        else if(playerLife <= lifeIndex)
        {
            bulbImage.sprite = PlayerLife.Instance.brokenBulbSprite;
        }
    }

}
