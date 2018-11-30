using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InputHandler : MonoBehaviour
{
    public Camera cam;
    public static InputHandler Instance;

    public TextMeshPro touchPosText;

    public List<int> usedFingerIdList;

    public bool touching;
    public Vector2 touchPos;

    private void Awake()
    {
        Instance = this;
    }

    public Vector3[] touchPosArray;
    private void Update()
    {
#if UNITY_ANDROID
        if(Input.touchCount > 0)
        {
            touching = true;
            touchPosText.text = "Touching";
            touchPosArray = new Vector3[Input.touchCount];
            for (int i = 0; i < Input.touchCount; i++)
            {
                touchPosArray[i] = cam.ScreenToWorldPoint(Input.touches[i].position);
                touchPosArray[i].z = 0;
            }
        }
        else
        {
            usedFingerIdList.Clear();
            touching = false;
            touchPosText.text = "Not Touching";
        }
#endif
    }

    public Vector3 MouseWorldPosition()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
