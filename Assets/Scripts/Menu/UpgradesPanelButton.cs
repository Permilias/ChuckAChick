using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesPanelButton : MonoBehaviour {


    public void ShowPanel()
    {
        UpgradesPanel.Instance.ShowPanel();
    }

    public void HidePanel()
    {
        UpgradesPanel.Instance.HidePanel();
    }
}
