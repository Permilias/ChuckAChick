using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakerChick : MonoBehaviour {

    public float detectionRadius;
    private void Start()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.eggbreakerChickAuraLoop);
    }
    private void Update()
    {
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
