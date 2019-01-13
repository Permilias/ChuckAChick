using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeButtonState
{
    locked,
    available,
    taken
}

public class UpgradeButton : MonoBehaviour {

    public int index;
    public SpriteRenderer SR;
    public UpgradeButtonState state;

    public int orderInBranch;
    public UpgradeButton branchButton1;
    public UpgradeButton branchButton2;

    [HideInInspector]
    public MenuButton button;

    public int currentCost;

    public int initialCost;
    public int secondCost;
    public int thirdCost;

    public int upgradeState;
    public string upgradeText1;
    public string upgradeText2;
    public string upgradeText3;
    public string upgradeText4;

    public bool selected;

    private void Start()
    {
        button = GetComponent<MenuButton>();
    }

    private void Update()
    {
        if (button.clicked)
        {
            if(!selected)
            {
                UpgradesManager.Instance.SelectButton(this);
            }
        }
    }

    public void RefreshAvailability()
    {
        upgradeState = UpgradesManager.Instance.upgradesArray[index];
        if (upgradeState == -1)
        {
            state = UpgradeButtonState.locked;
            currentCost = initialCost;
            SR.color = Color.black;
            if(orderInBranch == 0)
            {
                if (UpgradesManager.Instance.playerMoney >= currentCost)
                {
                    state = UpgradeButtonState.available;
                    upgradeState = 0;
                    UpgradesManager.Instance.upgradesArray[index] = 0;
                    SR.color = Color.gray;
                }
            }
            else if(orderInBranch == 1)
            {
                if(UpgradesManager.Instance.upgradesArray[branchButton1.index] > 0)
                {
                    if (UpgradesManager.Instance.playerMoney >= currentCost)
                    {
                        state = UpgradeButtonState.available;
                        upgradeState = 0;
                        UpgradesManager.Instance.upgradesArray[index] = 0;
                        SR.color = Color.gray;
                    }
                }
            }
            else
            {
                if (UpgradesManager.Instance.upgradesArray[branchButton2.index] > 0)
                {
                    if (UpgradesManager.Instance.playerMoney >= currentCost)
                    {
                        state = UpgradeButtonState.available;
                        upgradeState = 0;
                        UpgradesManager.Instance.upgradesArray[index] = 0;
                        SR.color = Color.gray;
                    }
                }
            }
        }
        else if (upgradeState == 0)
        {
            state = UpgradeButtonState.available;
            currentCost = initialCost;
            SR.color = Color.gray;
            if (orderInBranch == 0)
            {
                if (UpgradesManager.Instance.playerMoney < currentCost)
                {
                    state = UpgradeButtonState.locked;
                    currentCost = initialCost;
                    upgradeState = -1;
                    UpgradesManager.Instance.upgradesArray[index] = -1;
                    SR.color = Color.black;
                }
            }
            else if (orderInBranch == 1)
            {
                if (UpgradesManager.Instance.upgradesArray[branchButton1.index] > 0)
                {
                    if (UpgradesManager.Instance.playerMoney < currentCost)
                    {
                        state = UpgradeButtonState.locked;
                        currentCost = initialCost;
                        upgradeState = -1;
                        UpgradesManager.Instance.upgradesArray[index] = -1;
                        SR.color = Color.black;
                    }
                }
            }
            else
            {
                if (UpgradesManager.Instance.upgradesArray[branchButton2.index] > 0)
                {
                    if (UpgradesManager.Instance.playerMoney < currentCost)
                    {
                        state = UpgradeButtonState.locked;
                        currentCost = initialCost;
                        upgradeState = -1;
                        UpgradesManager.Instance.upgradesArray[index] = -1;
                        SR.color = Color.black;
                    }
                }
            }
        }
        else
        {
            state = UpgradeButtonState.taken;
            if (upgradeState == 1)
            {
                currentCost = secondCost;
            }
            else if (upgradeState == 2)
            {
                currentCost = thirdCost;
            }
            SR.color = Color.white;
        }
    }
}
