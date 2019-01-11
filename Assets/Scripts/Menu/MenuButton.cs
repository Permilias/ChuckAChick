using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour {

    public float radius;
    public bool clicked;
    public int fingerId;

    private void Update()
    {
        if(InputHandler.Instance.touching)
        {
            foreach (Touch touch in Input.touches)
            {
                Vector2 comparedPos = new Vector2(InputHandler.Instance.cam.ScreenToWorldPoint(touch.position).x, InputHandler.Instance.cam.ScreenToWorldPoint(touch.position).y);
                float dist = Vector2.Distance(comparedPos, transform.position);
                if (dist < radius)
                {
                    bool takesFingerId = true;
                    foreach (int i in InputHandler.Instance.usedFingerIdList)
                    {
                        if (i == touch.fingerId)
                        {
                            takesFingerId = false;
                            break;
                        }
                    }
                    if (takesFingerId)
                    {
                        clicked = true;
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

        if (Input.GetMouseButtonDown(0))
        {
            float dist = Vector2.Distance(InputHandler.Instance.MouseWorldPosition(), transform.position);
            if (dist < radius)
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
