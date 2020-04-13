using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class ItemNo : ScriptableObject
{
    public string myName;

    public bool isUgrade;
    public bool isNewWeapon;
    public bool isNewStats;

    public float myPercentage;
    public string rarity;

    public GameObject objectToSpawn;
}
