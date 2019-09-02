using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RichChick : MonoBehaviour {


    MagicChickAura aura;
    float radiusMultiplier;

    Color incomeColor;

    public float detectionRadius;

    public bool income;

    private void Awake()
    {
        aura = GetComponent<MagicChickAura>();
    }

    private void Start()
    {
        radiusMultiplier = detectionRadius / aura.auraTime;
        if (Time.timeSinceLevelLoad > 4f)
            SoundManager.Instance.PlaySound(SoundManager.Instance.godfatherChickAuraLoop);

        income = UpgradesApplier.Instance.richChickGeneratesIncome;
        incomeDelay = UpgradesApplier.Instance.richChickIncomeDelay;
        incomeColor = UpgradesApplier.Instance.richChickIncomeColor;
    }

    float incomeCount;
    float incomeDelay;
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

        if(income)
        {
            incomeCount += Time.deltaTime;
            if(incomeCount >= incomeDelay)
            {
                incomeCount = 0;
                GenerateIncome();
            }
        }
    }

    void GenerateIncome()
    {
        GameManager.Instance.AddScore(3);
        float size = 1.2f;
        NumberParticlesManager.Instance.SpawnNumberParticle(3, incomeColor, transform.position, 2, size, true);
    }
}
