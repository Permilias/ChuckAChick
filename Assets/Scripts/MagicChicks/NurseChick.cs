using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseChick : MonoBehaviour {

    MagicChickAura aura;
    float radiusMultiplier;

    private void Awake()
    {
        aura = GetComponent<MagicChickAura>();
    }

    public float detectionRadius;

    private void Start()
    {
        radiusMultiplier = detectionRadius / aura.auraTime;
        SoundManager.Instance.PlaySound(SoundManager.Instance.healChickAuraLoop);
    }
    private void Update()
    {
        detectionRadius = 0.7F + radiusMultiplier * aura.count;
        foreach (Chick chick in ChickGenerator.Instance.activeChicks)
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
