using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DowngradeBox : MonoBehaviour {

    MenuButton button;
    public static DowngradeBox Instance;

    private void Awake()
    {
        Instance = this;
        button = GetComponent<MenuButton>();
    }

    private void Update()
    {
        if (button.clicked)
        {
            button.clicked = false;
            UpgradesManager.Instance.Downgrade(UpgradesManager.Instance.currentSelectedButton);
        }
    }

}
