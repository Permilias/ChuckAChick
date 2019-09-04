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
    public bool canSootheBombs;

    private void Start()
    {
        radiusMultiplier = detectionRadius / aura.auraTime;
        if (Time.timeSinceLevelLoad > 4f)
            SoundManager.Instance.PlaySound(SoundManager.Instance.healChickAuraLoop);

        canSootheBombs = UpgradesApplier.Instance.healerDisableBombs;
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
                
                if(canSootheBombs)
                {
                    if(chick.bomb)
                    {
                        chick.Soothe();
                    }
                }
            }


        }
    }
}
