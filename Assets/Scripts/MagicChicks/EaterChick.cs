using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EaterChick : MonoBehaviour {

    public float detectionRadius;
    public int shellValue;
    public int value;
    public TextMeshPro valueText;

    Chick chick;

    private void Start()
    {
        value = 0;
        valueText.text = "0";
        chick = GetComponent<Chick>();
    }

    public void Eat(Vector3 shellStartingPosition)
    {
        value += shellValue;
        valueText.text = value.ToString();
        chick.value = value;
    }

    private void Update()
    {
        valueText.transform.position = transform.position + new Vector3(0, 0, -1);
        valueText.transform.rotation = Quaternion.identity;
    }
}
