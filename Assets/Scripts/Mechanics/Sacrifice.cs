using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum SacrificeType { loot, consumable, mechanic, debuff}
public class Sacrifice
{
    public int id;
    public int level;
    public string description;
    public SacrificeType type;

    public Sacrifice (int id, int level, string description, SacrificeType type)
    {
        this.id = id;
        this.level = level;
        this.description = description;
        this.type = type;
    }
    public Sacrifice(Sacrifice sacrifices)
    {
        this.id = sacrifices.id;
        this.level = sacrifices.id;
        this.description = sacrifices.description;
        this.type = sacrifices.type;
    }
}
