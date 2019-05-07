using System.Collections;
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

    public float auraTime;

    private void Start()
    {
        waveMain = wavePS.main;
        outlineShape = outlinePS.shape;
        paillettesShape = paillettesPS.shape;

        CalculateAuraTime();
        count = auraTime;
    }

    float wsMultiplier;
    float orMultiplier;
    float prMultiplier;

    public void CalculateAuraTime()
    {
        float wsDistance = waveStartingSize - waveMinSize;
        float orDistance = outlineStartingRadius - outlineMinRadius;
        float prDistance = paillettesStartingRadius - paillettesMinRadius;

        wsMultiplier = wsDistance / auraTime;
        orMultiplier = orDistance / auraTime;
        prMultiplier = prDistance / auraTime;
    }

    public void RefreshAura()
    {
        waveMain.startSize = waveMinSize + (wsMultiplier * count);
        outlineShape.radius = outlineMinRadius + (orMultiplier * count);
        paillettesShape.radius = paillettesMinRadius + (prMultiplier * count);
    }

    float count;
    private void Update()
    {
        if(count > 0)
        {
            count -= Time.deltaTime;
            RefreshAura();
        }
        else
        {
            count = 0;
        }
    }
}
