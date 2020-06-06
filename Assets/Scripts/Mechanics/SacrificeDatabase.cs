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

    public Sacrifice GetSacrifice(SacrificeType type, int intensity, int level) //specify item wanted and get it from database.
    {
        return sacrifices.Find(item => (item.type == type) && (item.intensity == intensity) && (item.level == level));
    }

    void BuildDataBase()
    {
        sacrifices = new List<Sacrifice>()
        {
            new Sacrifice(1, "common", loot, 0), //sacrifice 3 common items
            new Sacrifice(1, "rare", loot, 2), //sacrifice a rare item
            new Sacrifice(1, "potion", consumable, 0), //Sacrifice half of your potions.
            new Sacrifice(1, "mechanic", mechanic, 0), //Sacrifice your eye, reduces vision and impairs aiming.
            new Sacrifice(1, "mechanic", mechanic, 1), //Sacrifice a tendon, can no longer dodge.
            new Sacrifice(1, "debuff", debuff, 0), //Sacrifice your health.
            new Sacrifice(1, "debuff", debuff, 1), //Sacrifice your swiftness.
            new Sacrifice(2, "debuff", debuff, 2), //Sacrifice your strength.
            new Sacrifice(1, "debuff", debuff, 3), //Sacrifice your dexterity.

            new Sacrifice(2, "common", loot, 1), //sacrifice half of your common item
            new Sacrifice(2, "rare", loot, 3), //sacrifice 3 rare items
            new Sacrifice(2, "white", loot, 5), //sacrifice white light particle
            new Sacrifice(2, "violet", loot, 6), //sacrifice violet light particle
            new Sacrifice(2, "potion", consumable, 0), //Sacrifice half of your potions.
            new Sacrifice(2, "potion", consumable, 1), //Sacrifice all of your potions.
            new Sacrifice(2, "mechanic", mechanic, 0), //Sacrifice your eye, reduces vision and impairs aiming.
            new Sacrifice(2, "mechanic", mechanic, 1), //Sacrifice a tendon, can no longer dodge.
            new Sacrifice(2, "mechanic", mechanic, 2), //Sacrifice your arm, can no longer use shield.
            new Sacrifice(2, "debuff", debuff, 0), //Sacrifice your health.
            new Sacrifice(2, "debuff", debuff, 1), //Sacrifice your swiftness.
            new Sacrifice(2, "debuff", debuff, 2), //Sacrifice your strength.
            new Sacrifice(2, "debuff", debuff, 3), //Sacrifice your dexterity.

            new Sacrifice(3, "rare", loot, 3), //sacrifice 3 rare items
            new Sacrifice(3, "white", loot, 5), //Sacrifice your White light particle.
            new Sacrifice(3, "violet", loot, 6), //Sacrifice your Purple light particle
            new Sacrifice(3, "potion", consumable, 1), //Sacrifice all of your potions.
            new Sacrifice(3, "mechanic", mechanic, 2), //Sacrifice your arm, can no longer use shield.
            new Sacrifice(3, "mechanic", mechanic, 3), //Sacrifice your sword.
            new Sacrifice(3, "mechanic", mechanic, 4), //Sacrifice your crossbow.
            new Sacrifice(3, "debuff", debuff, 0), //Sacrifice your health.
            new Sacrifice(3, "debuff", debuff, 1), //Sacrifice your swiftness
            new Sacrifice(3, "debuff", debuff, 2), //Sacrifice your strength.
            new Sacrifice(3, "debuff", debuff, 3), //Sacrifice your dexterity.
            

            new Sacrifice(4, "rare", loot, 4), //sacrifice 7 rare items
            new Sacrifice(4, "legendary", loot, 7), // sacrifice legendary item
            new Sacrifice(4, "potion", consumable, 1), //Sacrifice all of your potions.
            new Sacrifice(4, "mechanic", mechanic, 3), //Sacrifice your sword.
            new Sacrifice(4, "mechanic", mechanic, 4), //Sacrifice your crossbow.
            new Sacrifice(4, "debuff", debuff, 4), //Suffer the curse of weakness.
            new Sacrifice(4, "debuff", debuff, 5), //Suffer the curse of frailty.

            new Sacrifice(5, "legendary", loot, 8), //sacrifice 2 legendary items
            new Sacrifice(5, "ancient", loot, 9), //Sacrifice ancient item
            new Sacrifice(5, "legendary ancient", loot, 10), //Sacrifice legendary item and ancient item
            new Sacrifice(5, "mechanic", mechanic, 3), //Sacrifice your sword.
            new Sacrifice(5, "mechanic", mechanic, 4), //Sacrifice your crossbow.
            new Sacrifice(5, "debuff", debuff, 4), //Suffer the curse of frailty.
            new Sacrifice(5, "debuff", debuff, 5) //Suffer the curse of weakness.
        };

    }
}
