using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*UPGRADES PATHS

A.
1. Poussin Healer
2. Les auras ne se depletent pas dans le rayon du poussin healer
3. Le poussin healer désamorçe les bombes

B.
4. Poussin Riche
5. Income poussin riche
6. 2 poussins riches

C.
7. Poussin Casseurs
8. Casse les oeufs magiques
9. Plus grande aura

D.
10. Poussin Vortex
11. Plus d'argent par poussin
12. Aspire aussi les poussins malades

*/
public class UpgradesApplier : MonoBehaviour {

	public static UpgradesApplier Instance;

    public int[] upgradesArray;

    public int factoryLevel;

    [Header("FIRST PATH")]
    public bool breakerBreaksMagic;
    public bool breakerHasBonusAura;
    public float breakerBonusAura;

    [Header("SECOND PATH")]
    public bool healerAuraBuff;
    public bool healerDisableBombs;

    [Header("THIRD PATH")]
    public bool vortexHasMoneyBonus;
    public int vortexMoneyBonus;
    public bool vortexEatsSick;

    [Header("FOURTH PATH")]

    public bool richChickGeneratesIncome;
    public float richChickIncomeDelay;
    public Color richChickIncomeColor;
    public bool spawns2RichChicks;

    private void Awake()
    {
        Instance = this;
    }

    public void ApplyUpgrades()
    {
        EggGenerator.Instance.availableIndexes = new List<int>();
        if(upgradesArray.Length == 12)
        {
            //FIRST PATH
            if(upgradesArray[0] <= 0)
            {
                print("No First Path");
                ChickGenerator.Instance.magicChickDatas[2].disabled = true;
            }
            else
            {
                EggGenerator.Instance.availableIndexes.Add(2);
                if (upgradesArray[1] <= 0)
                {
                    print("First Path Stops at 1");
                    breakerBreaksMagic = false;
                }
                else
                {
                    breakerBreaksMagic = true;

                    if (upgradesArray[2] <= 0)
                    {
                        print("First Path stops at 2");
                        breakerHasBonusAura = false;
                    }
                    else
                    {
                        breakerHasBonusAura = true;
                    }
                }
            }
            //SECOND PATH
            if (upgradesArray[3] <= 0)
            {
                print("No Second Path");
                ChickGenerator.Instance.magicChickDatas[0].disabled = true;
            }
            else
            {
                EggGenerator.Instance.availableIndexes.Add(0);

                if (upgradesArray[4] <= 0)
                {
                    print("Second Path Stops at 1");

                    healerDisableBombs = false;
                }
                else
                {
                    healerDisableBombs = true;


                    if (upgradesArray[5] <= 0)
                    {

                        print("Second Path stops at 2");
                        healerAuraBuff = false;

                    }
                    else
                    {
                        healerAuraBuff = true;

                    }
                }
            }
            //THIRD PATH
            if (upgradesArray[6] <= 0)
            {
                print("No Third Path");
                ChickGenerator.Instance.magicChickDatas[3].disabled = true;
            }
            else
            {
                EggGenerator.Instance.availableIndexes.Add(3);

                if (upgradesArray[7] <= 0)
                {
                    print("Third Path Stops at 1");
                    vortexHasMoneyBonus = false;
                }
                else
                {
                    vortexHasMoneyBonus = true;

                    if (upgradesArray[8] <= 0)
                    {
                        print("Third Path stops at 2");
                        vortexEatsSick = false;
                    }
                    else
                    {
                        vortexEatsSick = true;
                    }
                }
            }
            //FOURTH PATH
            if (upgradesArray[9] <= 0)
            {
                print("No Fourth Path");
                ChickGenerator.Instance.magicChickDatas[1].disabled = true;
            }
            else
            {
                EggGenerator.Instance.availableIndexes.Add(1);


                if (upgradesArray[10] <= 0)
                {
                    print("Fourth Path Stops at 1");

                    richChickGeneratesIncome = false;
                }
                else
                {

                    richChickGeneratesIncome = true;

                    if (upgradesArray[11] <= 0)
                    {
                        print("Fourth Path stops at 2");

                        spawns2RichChicks = false;
                    }
                    else
                    {

                        spawns2RichChicks = true;
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
