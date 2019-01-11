using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeButtonState
{
    locked,
    available,
    unavailable
}

public class UpgradeButton : MonoBehaviour {

    public int index;
    public UpgradeButtonState state;

    public void SetState()
    {
        int upgradeState = UpgradesManager.Instance.upgradesArray[index];
        if(upgradeState == -1)
        {
            state = UpgradeButtonState.locked;
        }
    }
}
