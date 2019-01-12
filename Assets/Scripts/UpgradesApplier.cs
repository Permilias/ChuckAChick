using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesApplier : MonoBehaviour {

	public static UpgradesApplier Instance;

    public int[] upgradesArray;

    private void Awake()
    {
        Instance = this;
    }
}
