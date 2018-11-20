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
#if UNITY_EDITOR

        print("editor");
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
#elif UNITY_ANDROID

        print("android");
        if(InputHandler.Instance.touching)
        {
            float dist = Vector2.Distance(InputHandler.Instance.touchPos, transform.position);
            if (dist < detectionRadius)
            {
                clicked = true;
            }
        }

        if(clicked)
        {
            if(Input.touches[0].phase == TouchPhase.Ended)
            {
                clicked = false;
            }
        }

#endif
    }
}
