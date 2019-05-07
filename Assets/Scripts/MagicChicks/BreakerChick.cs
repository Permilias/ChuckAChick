using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakerChick : MonoBehaviour {

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
        SoundManager.Instance.PlaySound(SoundManager.Instance.eggbreakerChickAuraLoop);
    }
    private void Update()
    {
        detectionRadius = 0.7f + radiusMultiplier * aura.count;
        foreach (Egg egg in EggGenerator.Instance.activeEggs)
        {
            float dist = Vector2.Distance(egg.transform.position, transform.position);
            if (dist <= detectionRadius)
            {
                if (!egg.magicEgg)
                {
                    egg.Break(false);
                    break;
                }
            }
        }
    }
}
