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

    [Header("FIRST PATH")]
    public bool healerAuraBuff;
    public bool healerDisableBombs;

    [Header("SECOND PATH")]
    public bool richChickGeneratesIncome;
    public int richChickIncome;
    public bool spawns2RichChicks;

    [Header("THIRD PATH")]
    public bool breakerBreaksMagic;
    public bool breakerHasBonusAura;
    public float breakerBonusAura;

    [Header("FOURTH PATH")]
    public bool vortexHasMoneyBonus;
    public int vortexMoneyBonus;
    public bool vortexEatsSick;

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
                ChickGenerator.Instance.magicChickDatas[0].eggOdds = 0;
            }
            else
            {
                ChickGenerator.Instance.magicChickDatas[0].eggOdds = 5;
                if(upgradesArray[1] <= 0)
                {
                    print("First Path Stops at 1");
                    healerAuraBuff = false;
                }
                else
                {
                    healerAuraBuff = true;
                    if(upgradesArray[2] <= 0)
                    {
                        print("First Path stops at 2");
                        healerDisableBombs = false;
                    }
                    else
                    {
                        healerDisableBombs = true;
                    }
                }
            }
            //SECOND PATH
            if (upgradesArray[3] <= 0)
            {
                print("No Second Path");
                ChickGenerator.Instance.magicChickDatas[1].eggOdds = 0;
            }
            else
            {
                ChickGenerator.Instance.magicChickDatas[1].eggOdds = 5;
                if (upgradesArray[4] <= 0)
                {
                    print("Second Path Stops at 1");
                    richChickGeneratesIncome = false;
                }
                else
                {
                    richChickGeneratesIncome = true;
                    if (upgradesArray[5] <= 0)
                    {
                        print("Second Path stops at 2");
                        spawns2RichChicks = false;
                    }
                    else
                    {
                        spawns2RichChicks = true;
                    }
                }
            }
            //THIRD PATH
            if (upgradesArray[6] <= 0)
            {
                print("No Third Path");
                ChickGenerator.Instance.magicChickDatas[2].eggOdds = 0;
            }
            else
            {
                ChickGenerator.Instance.magicChickDatas[2].eggOdds = 5;
                if (upgradesArray[7] <= 0)
                {
                    print("Third Path Stops at 1");
                    breakerBreaksMagic = false;
                }
                else
                {
                    breakerBreaksMagic = true;
                    if (upgradesArray[8] <= 0)
                    {
                        print("Third Path stops at 2");
                        breakerHasBonusAura = false;
                    }
                    else
                    {
                        breakerHasBonusAura = true;
                    }
                }
            }
            //FOURTH PATH
            if (upgradesArray[9] <= 0)
            {
                print("No Fourth Path");
                ChickGenerator.Instance.magicChickDatas[3].eggOdds = 0;
            }
            else
            {
                ChickGenerator.Instance.magicChickDatas[3].eggOdds = 5;
                if (upgradesArray[10] <= 0)
                {
                    print("Fourth Path Stops at 1");
                    vortexHasMoneyBonus = false;
                }
                else
                {
                    vortexHasMoneyBonus = true;
                    if (upgradesArray[11] <= 0)
                    {
                        print("Fourth Path stops at 2");
                        vortexEatsSick = false;
                    }
                    else
                    {
                        vortexEatsSick = true;
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
