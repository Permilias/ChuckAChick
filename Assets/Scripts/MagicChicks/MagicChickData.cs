using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MagicChickData
{
    public GameObject magicChick;
    public GameObject magicEgg;
    public GameObject shellExplosion;
    public Queue<GameObject> chickPool;
    public Queue<GameObject> eggPool;
    public Queue<GameObject> shellExplosionPool;
    public int eggOdds;
    public int eggHp;
    public int value;
    public Color scoreColor;
    public bool disabled;
}
