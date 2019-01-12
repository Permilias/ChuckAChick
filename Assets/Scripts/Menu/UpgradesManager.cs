using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour {

    public static UpgradesManager Instance;

    public int money;

    public int[] upgradesArray;

    public List<UpgradeButton> upgradeButtons;

    private void Awake()
    {
        Instance = this;
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
            button.SetState();
        }

    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.A))
        {
            upgradesArray[0] += 1;
        }
    }
}
