using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradesManager : MonoBehaviour {

    public bool resetsUpgrades;
    public static UpgradesManager Instance;

    public int playerMoney;

    public int[] upgradesArray;


    public List<UpgradeButton> upgradeButtons;

    public TextMeshProUGUI playerMoneyText;

    public TextMeshPro upgradeText;
    public TextMeshPro currentStateText;
    public TextMeshPro costText;
    public TextMeshPro titleText;

    public UpgradeButton currentSelectedButton;

    public GameObject selector;
    public float selectorSmallSize;
    public float selectorLargeSize;

    public Color upgradeColor;
    public Color downgradeColor;
    public float particleSizeUpgrade;
    public float particleSpeedUpgrade;
    public float particleSizeDowngrade;
    public float particleSpeedDowngrade;

    [Header("Factory Level")]
    public int factoryLevel;
    public FactoryLevelButton factoryButton;
    public bool factoryButtonSelected;
    public string level3text;

    public string level2text;

    public string level1text;

    public string level0text;

    public int[] factoryLevelCosts;

    private void Awake()
    {
        Instance = this;
    }

    
    int shownMoney;
    private void FixedUpdate()
    {
        if(shownMoney != playerMoney)
        {
            int dif = 10;
            if (Mathf.Abs(playerMoney - shownMoney) > 500)
            {
                dif = 60;
            }
            if (Mathf.Abs(playerMoney - shownMoney) > 5000)
            {
                dif = 120;
            }
            if (shownMoney < playerMoney)
            {
                for (int i = 0; i < dif; i++)
                {
                    shownMoney++;
                    if (shownMoney >= playerMoney)
                    {
                        shownMoney = playerMoney;
                        break;
                    }
                }
            }
            else if (shownMoney > playerMoney)
            {
                for (int i = 0; i < dif; i++)
                {
                    shownMoney--;
                    if (shownMoney <= playerMoney)
                    {
                        shownMoney = playerMoney;
                        break;
                    }
                }
            }


            playerMoneyText.text = shownMoney.ToString() + "$";
        }


    }

    public void UnselectAllButtons()
    {
        foreach (UpgradeButton button in upgradeButtons)
        {
            button.Unselect();
        }
        factoryButtonSelected = false;
        factoryButton.Unselect();
    }

    public void SelectFactoryButton()
    {
        UnselectAllButtons();
        factoryButton.Select();
        titleText.text = "FACTORY LEVEL";
        factoryButtonSelected = true;

        if(factoryLevel >= 3)
        {
            BuyButton.Instance.SetInactive();
            upgradeText.text = level3text;
            costText.text = "MAX";
        }
        else if(factoryLevel == 2)
        {
            upgradeText.text = level2text;
            costText.text = factoryLevelCosts[2].ToString() + "$";
            if(playerMoney >= factoryLevelCosts[2])
            {
                BuyButton.Instance.SetActionnable(true);
            }
            else
            {
                BuyButton.Instance.SetInactive();
            }
        }
        else if (factoryLevel == 1)
        {
            upgradeText.text = level1text;
            costText.text = factoryLevelCosts[1].ToString() + "$";
            if (playerMoney >= factoryLevelCosts[1])
            {
                BuyButton.Instance.SetActionnable(true);
            }
            else
            {
                BuyButton.Instance.SetInactive();
            }
        }
        else if (factoryLevel == 0)
        {
            upgradeText.text = level0text;
            costText.text = factoryLevelCosts[0].ToString() + "$";
            if (playerMoney >= factoryLevelCosts[0])
            {
                BuyButton.Instance.SetActionnable(true);
            }
            else
            {
                BuyButton.Instance.SetInactive();
            }
        }
    }

    public void UpgradeFactoryLevel()
    {
        if(factoryLevel < 3)
        {
            if (factoryLevelCosts[factoryLevel] <= playerMoney)
            {
                playerMoney -= factoryLevelCosts[factoryLevel];
                NumberParticlesManager.Instance.SpawnNumberParticle(-factoryLevelCosts[factoryLevel], Color.white, BuyButton.Instance.transform.position, particleSpeedUpgrade, particleSizeUpgrade, true);
                FactoryLevelButton.Instance.ChangeLevel(factoryLevel+1);
                SelectFactoryButton();
                foreach (UpgradeButton button in upgradeButtons)
                {
                    button.RefreshAvailability();
                }
            }
        }

    }

    public void SelectButton(UpgradeButton _button)
    {
        UnselectAllButtons();
        _button.Select();
        titleText.text = _button.title;

        currentSelectedButton = _button;
        if(_button.orderInBranch+1 > factoryLevel)
        {
            
            _button.upgradeState = -1;
            BuyButton.Instance.SetInactive();
            upgradeText.text = _button.upgradeText;
            costText.text = "LOCKED";
        }
        else
        {
            if (_button.upgradeState == -1)
            {
                BuyButton.Instance.SetInactive();

                upgradeText.text = _button.upgradeText;
                costText.text = _button.cost.ToString() + "$";
            }
            else if (_button.upgradeState == 0)
            {
                BuyButton.Instance.SetActionnable(true);

                upgradeText.text = _button.upgradeText;
                costText.text = _button.cost.ToString() + "$";
            }
            else if (_button.upgradeState == 1)
            {
                BuyButton.Instance.SetActionnable(false);

                upgradeText.text = _button.upgradeText;
                costText.text = "BOUGHT";
            }
        }

    }

    private void Start()
    {

        if (upgradesArray.Length == 0)
        {
            upgradesArray = new int[12];
            for(int i = 0; i < upgradesArray.Length; i++)
            {
                upgradesArray[i] = -1;
            }
        }

        if(resetsUpgrades)
        {
            for (int i = 0; i < upgradesArray.Length; i++)
            {
                upgradesArray[i] = -1;
            }
        }

        foreach(UpgradeButton button in upgradeButtons)
        {
            if(button.orderInBranch+1 > factoryLevel)
            {
                if(button.upgradeState >= 1)
                {
                    button.upgradeState = -1;
                    playerMoney += button.cost;
                }
                else if(button.upgradeState == 0)
                {
                    button.upgradeState = -1;
                }
            }
            button.RefreshAvailability();
        }


        currentStateText.text = "";
        upgradeText.text = "";
        costText.text = "";

        shownMoney = playerMoney;
        playerMoneyText.text = shownMoney.ToString() + "$";

    }

    public void Upgrade(UpgradeButton _button)
    {
            if (upgradesArray[_button.index] == 0)
            {
              if(playerMoney >= _button.cost)
                {
                    playerMoney -= _button.cost;
                    NumberParticlesManager.Instance.SpawnNumberParticle(-_button.cost, Color.white, BuyButton.Instance.transform.position, particleSpeedUpgrade, particleSizeUpgrade, true);
                upgradesArray[_button.index] = 1;
                    SoundManager.Instance.PlaySound(SoundManager.Instance.buyUpgradeSound);
                }
            }
        foreach (UpgradeButton button in upgradeButtons)
        {
            button.RefreshAvailability();
        }

        SelectButton(_button);

        DataManager.Instance.Save(false);


    }

    public void Downgrade(UpgradeButton _button)
    {
        if(upgradesArray[_button.index] == 1)
        {
            playerMoney += _button.cost;
            NumberParticlesManager.Instance.SpawnNumberParticle(_button.cost, Color.white, BuyButton.Instance.transform.position, particleSpeedUpgrade, particleSizeUpgrade, true);
            SoundManager.Instance.PlaySound(SoundManager.Instance.downgradeSound);

            if (_button.orderInBranch == 0)
            {
                Downgrade(_button.branchButton1);                    
            }
            else if(_button.orderInBranch == 1)
            {
                Downgrade(_button.branchButton2);
            }
        }

        upgradesArray[_button.index] = 0;

        foreach (UpgradeButton button in upgradeButtons)
        {
            button.RefreshAvailability();
        }

        SelectButton(_button);

        DataManager.Instance.Save(false);
    }
}