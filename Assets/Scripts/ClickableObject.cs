using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour
{
    public bool clicked;
    public float detectionRadius;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float dist = Vector2.Distance(InputHandler.Instance.MouseWorldPosition(), transform.position);
            if(dist < detectionRadius)
            {
                clicked = true;
            }
        }

        if (clicked)
        {
            if (Input.GetMouseButtonUp(0))
            {
                clicked = false;
            }
        }
    }
}
