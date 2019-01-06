using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RichChick : MonoBehaviour {

    public float detectionRadius;

    private void Update()
    {
        foreach (Chick chick in ChickGenerator.Instance.activeChicks)
        {
            float dist = Vector2.Distance(chick.transform.position, transform.position);
            if (dist <= detectionRadius)
            {
                if (!chick.bomb && !chick.magic)
                {
                    chick.Enrich();
                }
            }
        }
    }
}
