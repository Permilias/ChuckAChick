using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradesManager : MonoBehaviour {

    public static UpgradesManager Instance;

    public int playerMoney;

    public int[] upgradesArray;
    public int factoryLevel;

    public List<UpgradeButton> upgradeButtons;

    public TextMeshProUGUI playerMoneyText;

    public TextMeshPro upgradeText;
    public TextMeshPro currentStateText;
    public TextMeshPro costText;
    public TextMeshPro titleText;

    public UpgradeButton currentSelectedButton;

    public GameObject downgradeBox;

    public GameObject selector;
    public float selectorSmallSize;
    public float selectorLargeSize;

    public Color upgradeColor;
    public Color downgradeColor;
    public float particleSizeUpgrade;
    public float particleSpeedUpgrade;
    public float particleSizeDowngrade;
    public float particleSpeedDowngrade;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectButton(UpgradeButton _button)
    {
        foreach (UpgradeButton button in upgradeButtons)
        {
            button.Unselect();
        }
        _button.Select();
        titleText.text = _button.title;

        currentSelectedButton = _button;
        if (_button.upgradeState == -1)
        {
            BuyButton.Instance.SetInactive();
            upgradeText.text = _button.upgradeText;
            costText.text = _button.cost.ToString() + "$";
            currentStateText.text = "LOCKED";
        }
        else if (_button.upgradeState == 0)
        {
            BuyButton.Instance.SetActive(true);



            upgradeText.text = _button.upgradeText;
            costText.text = _button.cost.ToString() + "$";
            currentStateText.text = "AVAILABLE";
        }
        else if (_button.upgradeState == 1)
        {
            BuyButton.Instance.SetActive(false);
            upgradeText.text = _button.upgradeText;
            currentStateText.text = "ACTIVE";
            costText.text = _button.cost.ToString() + "$";




            //nextLevelText.text = "";
            /*upgradeText.text = _button.upgradeText2;
            currentLevelText.text = "CURRENT LEVEL : 1";
            costText.text = _button.secondCost.ToString() + "$";
            nextLevelText.text = "LVL 2";
            if(_button.orderInBranch == 0)
            {
                if(_button.branchButton1.upgradeState <= 0)
                {
                    downgradeBox.SetActive(true);
                }
                else
                {
                    downgradeBox.SetActive(false);
                }
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
                downgradeBox.SetActive(true);
                upgradeText.text = _button.upgradeText4;
                currentLevelText.text = "CURRENT LEVEL : 1";
                costText.text = "MAX";
                nextLevelText.text = "";
            }
        }
        else if (_button.upgradeState == 2)
        {
            upgradeText.text = _button.upgradeText3;
            currentLevelText.text = "CURRENT LEVEL : 2";
            costText.text = _button.thirdCost.ToString() + "$";
            nextLevelText.text = "LVL 3";
            downgradeBox.SetActive(true);
        }
        else
        {
            upgradeText.text = _button.upgradeText4;
            currentLevelText.text = "CURRENT LEVEL : 3";
            costText.text = "MAX";
            nextLevelText.text = "";
            downgradeBox.SetActive(true);*/
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

        playerMoneyText.text = playerMoney.ToString();

        //selector.SetActive(false);
        //downgradeBox.SetActive(false);

        currentStateText.text = "";
        upgradeText.text = "";
        costText.text = "";
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

        playerMoneyText.text = playerMoney.ToString();
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
                    upgradesArray[_button.branchButton1.index] = -1;
                }
                else if(_button.orderInBranch == 1)
                {
                    upgradesArray[_button.branchButton2.index] = -1;
                }
            }

        upgradesArray[_button.index] = 0;

            foreach (UpgradeButton button in upgradeButtons)
            {
                button.RefreshAvailability();
            }

            playerMoneyText.text = playerMoney.ToString();
            SelectButton(_button);

        DataManager.Instance.Save(false);
    }
}