using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesPanelButton : MonoBehaviour {

    MenuButton button;
    public bool shows;

    private void Awake()
    {
        button = GetComponent<MenuButton>();
    }

    private void Update()
    {
        if(button.clicked)
        {

            if(shows)
            {
                UpgradesPanel.Instance.ShowPanel();
            }
            else
            {
                UpgradesPanel.Instance.HidePanel();
            }
            button.clicked = false;
        }
    }
}
