﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagicChickAura : MonoBehaviour {

    public ParticleSystem wavePS;
    public ParticleSystem.MainModule waveMain;
    public float waveStartingSize;
    public float waveMinSize;
    public ParticleSystem outlinePS;
    public ParticleSystem.ShapeModule outlineShape;
    public float outlineStartingRadius;
    public float outlineMinRadius;
    public ParticleSystem paillettesPS;
    public ParticleSystem.ShapeModule paillettesShape;
    public float paillettesStartingRadius;
    public float paillettesMinRadius;
    public ParticleSystem vortexPS;
    public ParticleSystem.MainModule vortexMain;
    public float vortexStartingSize;
    public float vortexMinSize;

    public bool canDeplete;

    public float auraTime;

    private void Start()
    {
        waveMain = wavePS.main;
        outlineShape = outlinePS.shape;
        paillettesShape = paillettesPS.shape;
        vortexMain = vortexPS.main;

        healerAuraBuff = UpgradesApplier.Instance.healerAuraBuff;
    }

    public void StartAura(int multiplier)
    {
        canDeplete = true;
        CalculateAuraTime();
        count = auraTime * multiplier;
    }

    float wsMultiplier;
    float orMultiplier;
    float prMultiplier;
    float vsMultiplier;

    public void CalculateAuraTime()
    {
        float wsDistance = waveStartingSize - waveMinSize;
        float orDistance = outlineStartingRadius - outlineMinRadius;
        float prDistance = paillettesStartingRadius - paillettesMinRadius;
        float vsDistance = vortexStartingSize - vortexMinSize;

        wsMultiplier = wsDistance / auraTime;
        orMultiplier = orDistance / auraTime;
        prMultiplier = prDistance / auraTime;
        vsMultiplier = vsDistance / auraTime;
    }

    public void RefreshAura()
    {
        if(wavePS != null)
        {
            waveMain.startSize = waveMinSize + (wsMultiplier * count);
        }
        if(outlinePS != null)
        {
            outlineShape.radius = outlineMinRadius + (orMultiplier * count);
        }
        if(paillettesPS != null)
        {
            paillettesShape.radius = paillettesMinRadius + (prMultiplier * count);
        }
        if(vortexPS != null)
        {
            vortexMain.startSize = vortexMinSize + (vsMultiplier * count);
        }

    }

    public bool healerAuraBuff;
    public float count;
    private void Update()
    {
        if (healerAuraBuff)
        {
            canDeplete = true;
            foreach (NurseChick healer in ChickGenerator.Instance.activeHealers)
            {
                float dist = Vector2.Distance(healer.transform.position, transform.position);
                if (dist <= healer.detectionRadius)
                {
                    canDeplete = false;
                    break;
                }
            }
        }

        if(count > 0)
        {
            if(canDeplete)
            {
                count -= Time.deltaTime;
                RefreshAura();
            }
        }
        else
        {
            count = 0;
        }
    }
}
