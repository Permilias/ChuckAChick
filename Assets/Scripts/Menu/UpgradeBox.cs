using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBox : MonoBehaviour {

    MenuButton button;

    private void Awake()
    {
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
