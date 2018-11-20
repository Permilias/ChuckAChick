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

    public bool touching;
    public Vector2 touchPos;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
#if UNITY_ANDROID
        if(Input.touchCount > 0)
        {
            touching = true;
            touchPos = cam.ScreenToWorldPoint(Input.touches[0].position);
            touchPosText.text = touchPos.x.ToString() + " , " + touchPos.y.ToString();
        }
        else
        {
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
