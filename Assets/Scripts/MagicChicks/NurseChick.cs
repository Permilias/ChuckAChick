﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseChick : MonoBehaviour {

    public float detectionRadius;

    private void Start()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.healChickAuraLoop);
    }
    private void Update()
    {
        foreach(Chick chick in ChickGenerator.Instance.activeChicks)
        {
            float dist = Vector2.Distance(chick.transform.position, transform.position);
            if(dist <= detectionRadius)
            {
                if(chick.sick)
                {
                    chick.Heal();
                }
            }
        }
    }
}
