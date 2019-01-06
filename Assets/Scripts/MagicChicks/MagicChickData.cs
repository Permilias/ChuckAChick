using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MagicChickData
{
    public GameObject magicChick;
    public GameObject magicEgg;
    public Queue<GameObject> chickPool;
    public Queue<GameObject> eggPool;
    public int eggOdds;
    public int eggHp;
    public int value;
}
