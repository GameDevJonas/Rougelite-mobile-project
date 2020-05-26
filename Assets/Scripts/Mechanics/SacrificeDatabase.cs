using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SacrificeDatabase : MonoBehaviour
{
    public List<Sacrifice> sacrifices = new List<Sacrifice>();

    public SacrificeType loot;
    public SacrificeType consumable;
    public SacrificeType mechanic;
    public SacrificeType debuff;

    private void Start()
    {
        loot = SacrificeType.loot;
        consumable = SacrificeType.consumable;
        mechanic = SacrificeType.mechanic;
        debuff = SacrificeType.debuff;
        BuildDataBase();
    }
    
    /*public List<Sacrifice> GetSacrifice(int level)
    {
        List<Sacrifice> sacList = new List<Sacrifice>();

        foreach (var item in sacrifices)
        {
            for (int i = 0; i < sacrifices.Count; i++)
            {
                if (sacrifices[i].level == level)
                {
                    sacList.Add(sacrifices[i]);
                }
                else
                {
                    return sacList;
                }
            }
            return sacList;
        }
        return sacList;
    }*/

    public Sacrifice GetSacrifice(SacrificeType type, int intensity, int level)
    {
        return sacrifices.Find(item => (item.type == type) && (item.intensity == intensity) && (item.level == level));
    }

    void BuildDataBase()
    {
        sacrifices = new List<Sacrifice>()
        {
            new Sacrifice(0, 1, "common", loot, 0), //sacrifice 3 common items
            new Sacrifice(1, 1, "rare", loot, 0), //sacrifice a rare item
            new Sacrifice(2, 1, "Sacrifice half of your potions.", consumable, 0), //Sacrifice half of your potions.
            new Sacrifice(3, 1, "Sacrifice your eye, reduces vision and impairs aiming.", mechanic, 1), //Sacrifice your eye, reduces vision and impairs aiming.
            new Sacrifice(4, 1, "Sacrifice a tendon, can no longer dodge.", mechanic, 1),
            new Sacrifice(5, 1, "Sacrifice your health.", debuff, 1),
            new Sacrifice(6, 1, "Sacrifice your swiftness.", debuff, 1),
            new Sacrifice(7, 2, "Sacrifice 3", loot, 0), //sacrifice 3 rare items
            new Sacrifice(8, 2, "Sacrifice half of your", loot, 1), //sacrifice half of your common item
            new Sacrifice(9, 2, "Sacrifice your White light particle.", loot, 2),
            new Sacrifice(10, 2, "Sacrifice your Purple light particle", loot,3),
            new Sacrifice(11, 2, "Sacrifice all of your potions.", consumable, 0),
            new Sacrifice(12, 2, "Sacrifice half of your potions.", consumable, 1),
            new Sacrifice(13, 2, "Sacrifice your arm, can no longer use shield.", mechanic, 0),
            new Sacrifice(14, 2, "Sacrifice your eye, reduces vision and impairs aiming.", mechanic, 1),
            new Sacrifice(15, 2, "Sacrifice a tendon, can no longer dodge.", mechanic, 2),
            new Sacrifice(16, 2, "Sacrifice your strength.", debuff, 0),
            new Sacrifice(17, 2, "Sacrifice your dexterity.", debuff, 1),
            new Sacrifice(18, 2, "Sacrifice your health.", debuff, 2),
            new Sacrifice(19, 2, "Sacrifice your swiftness", debuff, 3),
            new Sacrifice(20, 3, "Sacrifice 3 ", loot, 0), //sacrifice 3 rare items
            new Sacrifice(21, 3, "Sacrifice your White light particle.", loot, 1),
            new Sacrifice(22, 3, "Sacrifice your Purple light particle", loot, 2),
            new Sacrifice(23, 3, "Sacrifice all of your potions.", consumable, 3),
            new Sacrifice(24, 3, "Sacrifice your sword.", mechanic, 4),
            new Sacrifice(25, 3, "Sacrifice your crossbow.", mechanic, 5),
            new Sacrifice(26, 3, "Sacrifice your arm, can no longer use shield.", mechanic, 6),
            new Sacrifice(27, 3, "Sacrifice your strength.", debuff, 0),
            new Sacrifice(28, 3, "Sacrifice your dexterity.", debuff, 1),
            new Sacrifice(29, 3, "Sacrifice your health.", debuff, 2),
            new Sacrifice(30, 3, "Sacrifice your swiftness", debuff,3),
            new Sacrifice(31, 4, "Sacrifice seven ", loot, 0), //sacrifice 7 rare items
            new Sacrifice(32, 4, "Sacrifice ", loot, 1), // sacrifice legendary item
            new Sacrifice(33, 4, "Sacrifice all of your potions.", consumable, 0),
            new Sacrifice(34, 4, "Sacrifice your sword.", mechanic, 1),
            new Sacrifice(35, 4, "Suffer the curse of weakness.", debuff, 2),
            new Sacrifice(36, 4, "Suffer the curse of frailty.", debuff, 3),
            new Sacrifice(37, 5, "Sacrifice all legendary items.", loot, 4),
            new Sacrifice(38, 5, "Sacrifice ", loot, 5), //Sacrifice ancient item
            new Sacrifice(39, 5, "Sacrifice ", loot, 6), //Sacrifice legendary item and ancient item
            new Sacrifice(40, 5, "Sacrifice your sword.", mechanic, 0),
            new Sacrifice(41, 5, "Sacrifice your crossbow.", mechanic, 1),
            new Sacrifice(42, 5, "Suffer the curse of weakness.", debuff, 2),
            new Sacrifice(43, 5, "Suffer the curse of frailty.", debuff, 3)
        };

    }
}
