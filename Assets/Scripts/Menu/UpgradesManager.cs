using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradesManager : MonoBehaviour {

    public static UpgradesManager Instance;

    public int playerMoney;

    public int[] upgradesArray;

    public List<UpgradeButton> upgradeButtons;

    public TextMeshPro playerMoneyText;
    public TextMeshPro playerMoneyText2;

    public TextMeshPro upgradeText;
    public TextMeshPro currentLevelText;
    public TextMeshPro costText;
    public TextMeshPro nextLevelText;

    public UpgradeButton currentSelectedButton;

    public GameObject downgradeBox;

    public GameObject selector;

    private void Awake()
    {
        Instance = this;
    }


    public void SelectButton(UpgradeButton _button)
    {
        foreach (UpgradeButton button in upgradeButtons)
        {
            button.selected = false;
        }
        _button.selected = true;
        selector.SetActive(true);
        selector.transform.position = _button.transform.position;
        currentSelectedButton = _button;
        if (_button.upgradeState == -1)
        {
            upgradeText.text = _button.upgradeText1;
            currentLevelText.text = "LVL 0";
            nextLevelText.text = "";
            costText.text = "LOCKED";
            downgradeBox.SetActive(false);
        }
        else if (_button.upgradeState == 0)
        {
            upgradeText.text = _button.upgradeText1;
            currentLevelText.text = "LVL 0";
            costText.text = _button.initialCost.ToString();
            nextLevelText.text = "LVL 1";
            downgradeBox.SetActive(false);
        }
        else if (_button.upgradeState == 1)
        {
            upgradeText.text = _button.upgradeText2;
            currentLevelText.text = "LVL 1";
            costText.text = _button.secondCost.ToString();
            nextLevelText.text = "LVL 2";
            if(_button.orderInBranch == 2)
            {
                downgradeBox.SetActive(true);
            }
            else if(_button.orderInBranch == 1)
            {
                if(_button.branchButton2.upgradeState < 1)
                {
                    downgradeBox.SetActive(true);
                }
                else
                {
                    downgradeBox.SetActive(false);
                }
            }
            else
            {
                if (_button.branchButton1.upgradeState < 1 && _button.branchButton2.upgradeState < 1)
                {
                    downgradeBox.SetActive(true);
                }
                else
                {
                    downgradeBox.SetActive(false);
                }
            }
            downgradeBox.SetActive(true);
        }
        else if (_button.upgradeState == 2)
        {
            upgradeText.text = _button.upgradeText3;
            currentLevelText.text = "LVL 2";
            costText.text = _button.thirdCost.ToString();
            nextLevelText.text = "LVL 3";
            downgradeBox.SetActive(true);
        }
        else
        {
            upgradeText.text = _button.upgradeText4;
            currentLevelText.text = "LVL 3";
            costText.text = "MAX";
            nextLevelText.text = "";
            downgradeBox.SetActive(true);
        }
    }

    private void Start()
    {
        if(upgradesArray.Length == 0)
        {
            upgradesArray = new int[12];
            for(int i = 0; i < upgradesArray.Length; i++)
            {
                upgradesArray[i] = -1;
            }
        }

        foreach(UpgradeButton button in upgradeButtons)
        {
            button.RefreshAvailability();
        }

        playerMoneyText.text = "Player Money : " + playerMoney.ToString();
        playerMoneyText2.text = "Player Money : " + playerMoney.ToString();

        selector.SetActive(false);
    }

    public void Upgrade(UpgradeButton _button)
    {
        if (upgradesArray[_button.index] < 3)
        {
            if (upgradesArray[_button.index] == 0)
            {
                playerMoney -= _button.initialCost;
            }
            else if (upgradesArray[_button.index] == 1)
            {
                playerMoney -= _button.secondCost;
            }
            else
            {
                playerMoney -= _button.thirdCost;
            }
            upgradesArray[_button.index] += 1;
        }
        foreach (UpgradeButton button in upgradeButtons)
        {
            button.RefreshAvailability();
        }

        playerMoneyText.text = "Player Money : " + playerMoney.ToString();
        playerMoneyText2.text = "Player Money : " + playerMoney.ToString();
        SelectButton(_button);
    }

    public void Downgrade(UpgradeButton _button)
    {
        if(upgradesArray[_button.index] > 0)
        {
            if(upgradesArray[_button.index] == 1)
            {
                playerMoney += _button.initialCost;
                if(_button.orderInBranch == 0)
                {
                    upgradesArray[_button.branchButton1.index] = -1;
                }
                else if(_button.orderInBranch == 1)
                {
                    upgradesArray[_button.branchButton2.index] = -1;
                }
            }
            else if(upgradesArray[_button.index] == 2)
            {
                playerMoney += _button.secondCost;
            }
            else
            {
                playerMoney += _button.thirdCost;
            }
            upgradesArray[_button.index] -= 1;


            foreach (UpgradeButton button in upgradeButtons)
            {
                button.RefreshAvailability();
            }

            playerMoneyText.text = "Player Money : " + playerMoney.ToString();
            playerMoneyText2.text = "Player Money : " + playerMoney.ToString();
            SelectButton(_button);
        }
    }
}
