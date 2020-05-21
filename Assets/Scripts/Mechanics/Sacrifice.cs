using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SacrificeType { loot, consumable, mechanic, debuff}
[Serializable]
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
    public Sacrifice(Sacrifice sacrifice)
    {
        this.id = sacrifice.id;
        this.level = sacrifice.level;
        this.description = sacrifice.description;
        this.type = sacrifice.type;
    }
}
