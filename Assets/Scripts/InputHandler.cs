using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    public Camera cam;
    public static InputHandler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Vector3 MouseWorldPosition()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
