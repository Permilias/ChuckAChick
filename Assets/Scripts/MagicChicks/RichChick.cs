using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RichChick : MonoBehaviour {


    MagicChickAura aura;
    float radiusMultiplier;

    public float detectionRadius;

    private void Awake()
    {
        aura = GetComponent<MagicChickAura>();
    }

    private void Start()
    {
        radiusMultiplier = detectionRadius / aura.auraTime;
        SoundManager.Instance.PlaySound(SoundManager.Instance.godfatherChickAuraLoop);
    }

    private void Update()
    {
        detectionRadius = 0.7F + radiusMultiplier * aura.count;
        foreach (Chick chick in ChickGenerator.Instance.activeChicks)
        {
            float dist = Vector2.Distance(chick.transform.position, transform.position);
            if (dist <= detectionRadius)
            {
                if (!chick.bomb && !chick.magic && !chick.sick)
                {
                    chick.Enrich();
                }
            }
        }
    }
}
