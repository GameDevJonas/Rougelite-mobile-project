using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item
{
    public int id;
    public string tier;
    public string title;
    public string lore;
    public string description;
    public CharacterStat statType;
    public string modType;
    //public Sprite icon;
    public float statValue;
    public Item(int id, string tier, string title, string lore, string description, /*icon,*/ CharacterStat statType, string modType,float statValue)
    {
        this.id = id;
        this.tier = tier;
        this.title = title;
        this.lore = lore;
        this.description = description;
        //this.icon = Resources.Load<Sprite>(Sprite/Items + title);
        this.statType = statType;
        this.modType = modType;
        this.statValue = statValue;
    }
    public Item(Item item)
    {
        this.id = item.id;
        this.tier = item.tier;
        this.title = item.title;
        this.lore = item.lore;
        this.description = item.description;
        //this.icon = Resources.Load<Sprite>(Sprite/Items + item.title);
        this.statType = item.statType;
        this.modType = item.modType;
        this.statValue = item.statValue;
    }
}
