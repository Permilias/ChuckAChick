using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBox : MonoBehaviour {

    MenuButton button;
    public static UpgradeBox Instance;


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
            if(UpgradesManager.Instance.currentSelectedButton.upgradeState > -1)
            {
                UpgradesManager.Instance.Upgrade(UpgradesManager.Instance.currentSelectedButton);
            }
        }
    }
}
