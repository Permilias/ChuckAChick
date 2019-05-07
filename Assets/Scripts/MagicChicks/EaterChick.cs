using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EaterChick : MonoBehaviour {

    public float detectionRadius;
    public int shellValue;
    public int value;
    public TextMeshPro valueText;


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
        SoundManager.Instance.PlaySound(SoundManager.Instance.vortexChickAuraLoop);
        value = 0;
        valueText.text = "0";
        chick = GetComponent<Chick>();
    }

    public void Eat(Vector3 chickStartingPosition)
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.vortexChickIncrementation);
        value += shellValue;
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
