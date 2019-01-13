using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*UPGRADES PATHS

A : TW
1. Les poussins valent plus
2. Chuck une bombe rapporte de l'argent
3. Les poussins riches génèrent de l'income

B : CW
4. Casser un oeuf rapporte
5. Casser un oeuf soigne, mais faire tomber un oeuf est plus dangereux
6. Les poussins eater ont un effet global

C : TF
7. Les poussins malades pénalisent moins
8. Les poussins magiques sont plus fréquents
9. Les poussins soigneurs désamorcent les bombes

D. CF
10. Plus de vie pour l'usine
11. Les oeufs magiques ont 1 PV
12. Les poussins casseurs ont plus de portée

*/
public class UpgradesApplier : MonoBehaviour {

	public static UpgradesApplier Instance;

    public int[] upgradesArray;

    [Header("FIRST PATH")]
    public float chickValueUpgradeMultiplier;
    public float bombChickChuckingUpgradeMultiplier;

    [Header("SECOND PATH")]
    public float breakingMoneyMultiplier;
    public float breakingHealingMultiplier;

    [Header("THIRD PATH")]
    public int sickChickReductionMultiplier;
    public float magicEggFrequencyReductionMultiplier;

    [Header("FOURTH PATH")]
    public int lifeUpgradeMultiplier;
    public float shockwaveRadiusMultiplier;

    private void Awake()
    {
        Instance = this;
    }

    public void ApplyUpgrades()
    {
        if(upgradesArray.Length == 12)
        {
            //FIRST PATH
            if(upgradesArray[0] <= 0)
            {
                print("No First Path");
            }
            else
            {
                ChickGenerator.Instance.baseChickValue += (chickValueUpgradeMultiplier * upgradesArray[0]);
                if(upgradesArray[1] <= 0)
                {
                    print("First Path Stops at 1");
                }
                else
                {
                    Chucker.Instance.chuckingBombGivesMoney = true;
                    Chucker.Instance.bombChickChuckingValue = bombChickChuckingUpgradeMultiplier * upgradesArray[1];
                    if(upgradesArray[2] <= 0)
                    {
                        print("First Path stops at 2");
                    }
                    else
                    {
                        //A COMPLETER
                    }
                }
            }
            //SECOND PATH
            if (upgradesArray[3] <= 0)
            {
                print("No Second Path");
            }
            else
            {
                EggGenerator.Instance.breakingGivesMoney = true;
                EggGenerator.Instance.breakingMoney = breakingMoneyMultiplier * upgradesArray[3];
                if (upgradesArray[4] <= 0)
                {
                    print("Second Path Stops at 1");
                }
                else
                {
                    EggGenerator.Instance.breakingHeals = true;
                    EggGenerator.Instance.breakingHealing = breakingHealingMultiplier * upgradesArray[4];
                    if (upgradesArray[5] <= 0)
                    {
                        print("Second Path stops at 2");
                    }
                    else
                    {
                        //A COMPLETER
                    }
                }
            }
            //THIRD PATH
            if (upgradesArray[6] <= 0)
            {
                print("No Third Path");
            }
            else
            {
                ChickGenerator.Instance.baseSickChickValue -= (sickChickReductionMultiplier * upgradesArray[6]);
                if (upgradesArray[7] <= 0)
                {
                    print("Third Path Stops at 1");
                }
                else
                {
                    EggGenerator.Instance.magicEggFrequency -= (magicEggFrequencyReductionMultiplier * upgradesArray[8]);
                    if (upgradesArray[8] <= 0)
                    {
                        print("Third Path stops at 2");
                    }
                    else
                    {
                        //A COMPLETER
                    }
                }
            }
            //FOURTH PATH
            if (upgradesArray[9] <= 0)
            {
                print("No Fourth Path");
            }
            else
            {
                PlayerLife.Instance.maxLife += (lifeUpgradeMultiplier * upgradesArray[9]);
                PlayerLife.Instance.life = PlayerLife.Instance.maxLife;
                if (upgradesArray[10] <= 0)
                {
                    print("Fourth Path Stops at 1");
                }
                else
                {
                    EggGenerator.Instance.shockwaves = true;
                    EggGenerator.Instance.shockwaveRadius = shockwaveRadiusMultiplier * upgradesArray[10];
                    if (upgradesArray[11] <= 0)
                    {
                        print("Fourth Path stops at 2");
                    }
                    else
                    {
                        //A COMPLETER
                    }
                }
            }
        }
        else
        {
            print("Upgrades Array Absent");
        }
    }
}
