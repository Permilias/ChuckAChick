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

    public int cost;

    public int upgradeState;
    public string title;
    public string upgradeText;

    public Sprite lockedSprite;
    public Sprite availableSprite;
    public Sprite unlockedSprite;
    public Sprite pushAvailableSprite;
    public Sprite pushUnlockedSprite;

    public bool selected;



    private void Start()
    {
        button = GetComponent<MenuButton>();
    }

    private void Update()
    {
        if (button.clicked)
        {
            button.clicked = false;
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
            SR.sprite = lockedSprite;
            if(orderInBranch == 0)
            {
                if (UpgradesManager.Instance.playerMoney >= cost)
                {
                    state = UpgradeButtonState.available;
                    upgradeState = 0;
                    UpgradesManager.Instance.upgradesArray[index] = 0;
                    if(selected)
                    {
                        SR.sprite = pushAvailableSprite;
                    }
                    else
                    {
                        SR.sprite = availableSprite;
                    }

                }
            }
            else if(orderInBranch == 1)
            {
                if(UpgradesManager.Instance.upgradesArray[branchButton1.index] > 0)
                {
                    if (UpgradesManager.Instance.playerMoney >= cost)
                    {
                        state = UpgradeButtonState.available;
                        upgradeState = 0;
                        UpgradesManager.Instance.upgradesArray[index] = 0;
                        if (selected)
                        {
                            SR.sprite = pushAvailableSprite;
                        }
                        else
                        {
                            SR.sprite = availableSprite;
                        }
                    }
                }
            }
            else
            {
                if (UpgradesManager.Instance.upgradesArray[branchButton2.index] > 0)
                {
                    if (UpgradesManager.Instance.playerMoney >= cost)
                    {
                        state = UpgradeButtonState.available;
                        upgradeState = 0;
                        UpgradesManager.Instance.upgradesArray[index] = 0;
                        if (selected)
                        {
                            SR.sprite = pushAvailableSprite;
                        }
                        else
                        {
                            SR.sprite = availableSprite;
                        }
                    }
                }
            }
        }
        else if (upgradeState == 0)
        {
            state = UpgradeButtonState.available;
            SR.sprite = availableSprite;
            if (orderInBranch == 0)
            {
                if (UpgradesManager.Instance.playerMoney < cost)
                {
                    state = UpgradeButtonState.locked;
                    upgradeState = -1;
                    UpgradesManager.Instance.upgradesArray[index] = -1;
                    SR.sprite = lockedSprite;
                }
            }
            else if (orderInBranch == 1)
            {
                if (UpgradesManager.Instance.upgradesArray[branchButton1.index] > 0)
                {
                    if (UpgradesManager.Instance.playerMoney < cost)
                    {
                        state = UpgradeButtonState.locked;
                        upgradeState = -1;
                        UpgradesManager.Instance.upgradesArray[index] = -1;
                        SR.sprite = lockedSprite;
                    }
                }
            }
            else
            {
                if (UpgradesManager.Instance.upgradesArray[branchButton2.index] > 0)
                {
                    if (UpgradesManager.Instance.playerMoney < cost)
                    {
                        state = UpgradeButtonState.locked;
                        upgradeState = -1;
                        UpgradesManager.Instance.upgradesArray[index] = -1;
                        SR.sprite = lockedSprite;
                    }
                }
            }
        }
        else if(upgradeState == 1)
        {
            if (selected)
            {
                SR.sprite = pushUnlockedSprite;
            }
            else
            {
                SR.sprite = unlockedSprite;
            }
        }
    }

    public void Select()
    {
        selected = true;
        if(SR.sprite == availableSprite)
        {
            SR.sprite = pushAvailableSprite;
        }
        else if(SR.sprite == unlockedSprite)
        {
            SR.sprite = pushUnlockedSprite;
        }
    }

    public void Unselect()
    {
        selected = false;
        if (SR.sprite == pushAvailableSprite)
        {
            SR.sprite = availableSprite;
        }
        else if (SR.sprite == pushUnlockedSprite)
        {
            SR.sprite = unlockedSprite;
        }
    }
}
