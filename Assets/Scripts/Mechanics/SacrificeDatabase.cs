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
    
    public Sacrifice GetSacrifice(int id)
    {
        return sacrifices.Find(sacrifices => sacrifices.id == id);
    }

    void BuildDataBase()
    {
        sacrifices = new List<Sacrifice>()
        {
            new Sacrifice(0, 1, "Sacrifice 3 ", loot), //sacrifice 3 common items
            new Sacrifice(1, 1, "Sacrifice a", loot), //sacrifice a rare item
            new Sacrifice(2, 1, "Sacrifice half of your potions.", consumable),
            new Sacrifice(3, 1, "Sacrifice your eye, reduces vision and impairs aiming.", mechanic),
            new Sacrifice(4, 1, "Sacrifice a tendon, can no longer dodge.", mechanic),
            new Sacrifice(5, 1, "Sacrifice your health.", debuff),
            new Sacrifice(6, 1, "Sacrifice your swiftness.", debuff),
            new Sacrifice(7, 2, "Sacrifice 3", loot), //sacrifice 3 rare items
            new Sacrifice(8, 2, "Sacrifice half of your", loot), //sacrifice half of your common item
            new Sacrifice(9, 2, "Sacrifice your White light particle.", loot),
            new Sacrifice(10, 2, "Sacrifice your Purple light particle", loot),
            new Sacrifice(11, 2, "Sacrifice all of your potions.", consumable),
            new Sacrifice(12, 2, "Sacrifice half of your potions.", consumable),
            new Sacrifice(13, 2, "Sacrifice your arm, can no longer use shield.", mechanic),
            new Sacrifice(14, 2, "Sacrifice your eye, reduces vision and impairs aiming.", mechanic),
            new Sacrifice(15, 2, "Sacrifice a tendon, can no longer dodge.", mechanic),
            new Sacrifice(16, 2, "Sacrifice your strength.", debuff),
            new Sacrifice(17, 2, "Sacrifice your dexterity.", debuff),
            new Sacrifice(18, 2, "Sacrifice your health.", debuff),
            new Sacrifice(19, 2, "Sacrifice your swiftness", debuff),
            new Sacrifice(20, 3, "Sacrifice 3 ", loot), //sacrifice 3 rare items
            new Sacrifice(21, 3, "Sacrifice your White light particle.", loot),
            new Sacrifice(22, 3, "Sacrifice your Purple light particle", loot),
            new Sacrifice(23, 3, "Sacrifice all of your potions.", consumable),
            new Sacrifice(24, 3, "Sacrifice your sword.", mechanic),
            new Sacrifice(25, 3, "Sacrifice your crossbow.", mechanic),
            new Sacrifice(26, 3, "Sacrifice your arm, can no longer use shield.", mechanic),
            new Sacrifice(27, 3, "Sacrifice your strength.", debuff),
            new Sacrifice(28, 3, "Sacrifice your dexterity.", debuff),
            new Sacrifice(29, 3, "Sacrifice your health.", debuff),
            new Sacrifice(30, 3, "Sacrifice your swiftness", debuff),
            new Sacrifice(31, 4, "Sacrifice seven ", loot), //sacrifice 7 rare items
            new Sacrifice(32, 4, "Sacrifice ", loot), // sacrifice legendary item
            new Sacrifice(33, 4, "Sacrifice all of your potions.", consumable),
            new Sacrifice(34, 4, "Sacrifice your sword.", mechanic),
            new Sacrifice(35, 4, "Suffer the curse of weakness.", debuff),
            new Sacrifice(36, 4, "Suffer the curse of frailty.", debuff),
            new Sacrifice(37, 5, "Sacrifice all legendary items.", loot),
            new Sacrifice(38, 5, "Sacrifice ", loot), //Sacrifice ancient item
            new Sacrifice(39, 5, "Sacrifice ", loot), //Sacrifice legendary item and ancient item
            new Sacrifice(40, 5, "Sacrifice your sword.", mechanic),
            new Sacrifice(41, 5, "Sacrifice your crossbow.", mechanic),
            new Sacrifice(42, 4, "Suffer the curse of weakness.", debuff),
            new Sacrifice(43, 4, "Suffer the curse of frailty.", debuff)
        };

    }
}
