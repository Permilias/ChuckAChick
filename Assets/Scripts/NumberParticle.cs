using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberParticle : MonoBehaviour {

    Animator anim;
    public TextMeshPro text;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Play(float value, Color color, float speed, float size, bool money)
    {
        if(money)
        {
            if(value >= 0)
            {
                text.text = "+" + Mathf.RoundToInt(value).ToString() + "$";
            }
            else
            {
                text.text = Mathf.RoundToInt(value).ToString() + "$";
            }
        }
        else
        {
            if (value >= 0)
            {
                text.text = "+" + Mathf.RoundToInt(value).ToString() + "HP";
            }
            else
            {
                text.text = Mathf.RoundToInt(value).ToString() + "HP";
            }
        }
        transform.localScale = new Vector3(size, size, size);
        anim.speed = speed;
        anim.SetTrigger("play");
        text.color = color;
    }
}
