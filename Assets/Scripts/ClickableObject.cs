using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour
{
    public bool clicked;
    public float detectionRadius;

    public int fingerId = -1;


    private void Update()
    {

        //#if UNITY_ANDROID

        if (InputHandler.Instance.touching && fingerId == -1)
        {
            foreach(Touch touch in Input.touches)
            {
                Vector2 comparedPos = new Vector2(InputHandler.Instance.cam.ScreenToWorldPoint(touch.position).x, InputHandler.Instance.cam.ScreenToWorldPoint(touch.position).y);
                float dist = Vector2.Distance(comparedPos, transform.position);
                if (dist < detectionRadius)
                {
                    bool takesFingerId = true;
                    foreach(int i in InputHandler.Instance.usedFingerIdList)
                    {
                        if(i == touch.fingerId)
                        {
                            takesFingerId = false;
                            break;
                        }
                    }
                    if(takesFingerId)
                    {
                        if(!GameManager.Instance.gameEnded)
                        {
                            clicked = true;
                        }
                        fingerId = touch.fingerId;
                        InputHandler.Instance.usedFingerIdList.Add(fingerId);
                    }
                    break;
                }
            }
        }

        if (clicked)
        {
            foreach (Touch touch in Input.touches)
            {

                if (touch.fingerId == fingerId)
                {
                    if (touch.phase == TouchPhase.Ended)
                    {
                        InputHandler.Instance.usedFingerIdList.Remove(fingerId);
                        fingerId = -1;
                        clicked = false;
                    }
                }
            }
        }



        //#elif UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            float dist = Vector2.Distance(InputHandler.Instance.MouseWorldPosition(), transform.position);
            if (dist < detectionRadius)
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
//#endif
    }
}
