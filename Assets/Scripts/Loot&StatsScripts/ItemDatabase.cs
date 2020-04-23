using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public CharacterStat Health;
    public CharacterStat HealthPercent;
    public CharacterStat Strength;
    public CharacterStat StrengthPercent;
    public CharacterStat Dexterity;
    public CharacterStat CritChance;
    public CharacterStat CritDamage;
    public CharacterStat LifeOnHit;
    public CharacterStat LifeOnHitPercent;
    public CharacterStat SwordAttackModifier;
    public CharacterStat CrossbowAttackModifier;
    public CharacterStat PotionPotency;
    public CharacterStat MovementSpeed;

    private void Start()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        PlayerStats playerstats = Player.GetComponent<PlayerStats>();
        Health = playerstats.Health;
        HealthPercent = playerstats.HealthPercent;
        Strength = playerstats.Strength;
        StrengthPercent = playerstats.StrengthPercent;
        Dexterity = playerstats.Dexterity;
        CritChance = playerstats.CritChance;
        CritDamage = playerstats.CritDamage;
        LifeOnHit = playerstats.LifeOnHitPercent;
        SwordAttackModifier = playerstats.SwordAttackModifier;
        CrossbowAttackModifier = playerstats.CrossbowAttackModifier;
        PotionPotency = playerstats.PotionPotency;
        MovementSpeed = playerstats.MovementSpeed;
        BuildDatabase();
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }

    void BuildDatabase()
    {
        items = new List<Item>()
        {
            new Item(0, "Common Loot", "Pale Scale", "A white scale from an unknown creature. Increases health.", "+ 25 Health", Health, 25),
            new Item(1, "Common Loot", "Sharp Tooth", "A long and pointy tooth, it seems demonic. Increases strength.", "+ 1 Strength", Strength, 1)
        };
    }
}
