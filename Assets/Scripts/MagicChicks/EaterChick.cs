using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EaterChick : MonoBehaviour {

    public float detectionRadius;
    public int shellValue;
    public int richShellValue;
    public int value;
    public TextMeshPro valueText;
    public bool multiplier;

    MagicChickAura aura;

    private void Awake()
    {
        aura = GetComponent<MagicChickAura>();
    }

    float radiusMultiplier;
    Chick chick;

    private void Start()
    {
        radiusMultiplier = detectionRadius / aura.auraTime;
        if (Time.timeSinceLevelLoad > 4f)
            SoundManager.Instance.PlaySound(SoundManager.Instance.vortexChickAuraLoop);
        value = 0;
        valueText.text = "0";
        chick = GetComponent<Chick>();

        multiplier = UpgradesApplier.Instance.vortexHasMoneyBonus;
    }

    public void Eat(Vector3 chickStartingPosition, bool rich)
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.vortexChickIncrementation);
        int bonus = 0;
        if (rich) bonus = richShellValue;
        else bonus = shellValue;
        if (multiplier) bonus *= 2;
        value += bonus;
        valueText.text = value.ToString();
        chick.value = value;
    }

    private void Update()
    {
        detectionRadius = 0.7F + radiusMultiplier * aura.count;
        valueText.transform.position = transform.position + new Vector3(0, 0, -3);
        valueText.transform.rotation = Quaternion.identity;
    }
}
