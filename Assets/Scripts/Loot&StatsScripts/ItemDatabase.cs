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
        Strength = playerstats.Strength;
        Dexterity = playerstats.Dexterity;
        CritChance = playerstats.CritChance;
        CritDamage = playerstats.CritDamage;
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
            new Item(0, "Common Loot", "Pale Scale", "A white scale from an unknown creature. Increases health.", "+ 25 Health", Health, "flat", 25),
            new Item(1, "Common Loot", "Sharp Tooth", "A long and pointy tooth, it seems demonic. Increases strength.", "+ 1 Strength", Strength, "flat", 1),
            new Item(2, "Common Loot", "Red String", "Hard to identify, but it is organic. Increases dexterity.", "+ 1 Dexterity", Dexterity, "flat", 1),
            new Item(3, "Common Loot", "Shrunken Leech", "Shriveled, but still abnormally big. Increases life on hit.", "+ 1 Life on hit", LifeOnHit, "flat", 1),
            new Item(4, "Common Loot", "Luminescent Black Mushroom", "A paradoxical and unnatural mushroom. Increases potion potency.", "+ 10 Potion Potency", PotionPotency, "flat", 10),
            new Item(5, "Rare Loot", "Pale Skin",  "White and scaled skin which looks like snakeskin but has protrusions and holes which suggest otherwise. Increases health.", "+ 10 % Health", Health, "percent", 0.1f),
            new Item(6, "Rare Loot", "Broken Horn", "A horn resembling a goat but seems predatory and evil… Seems like these pieces originate from the same creature. Increases attack damage.", "+ 10 % Strength", Strength, "percent", 0.1f),
            new Item(7, "Rare Loot", "Red Guts", "Apparently intestines, but from what? Increases attack speed.", "+ 10 % Dexterity", Dexterity, "percent", 0.1f),
            new Item(8, "Rare Loot", "Stained Nail", "Feels like a plant, but hard as iron. Stained with dried liquids. Increases critical strike chance.", "+ 5 % Critical strike chance", CritChance, "flat", 5),
            new Item(9, "Rare Loot", "Hair fetish", "Small occult looking doll. It seems to have the power to inflict great pain. Increases critical strike damage.", "+ 10 % Critical strike damage", CritDamage, "flat", 10),
            new Item(10, "Rare Loot", "Magnetic dust", "A hot magnetic powder clinging to sword blades; however, it does not attach to other metals. Increases sword damage.", "+ 25 % sword damage", SwordAttackModifier, "percent", 0.25f),
            new Item(11, "Rare Loot", "Fetid wax", "Nauseating smell, but it is very smooth when applied to the crossbow barrel. Increases crossbow damage.", "+ 25 % crossbow damage", CrossbowAttackModifier, "percent", 0.25f),
            new Item(12, "Rare Loot", "Pulsating something", "Mysteriously irresistible, invigorating when eaten. Increases movement speed.", "+ 5 % movement speed", MovementSpeed, "percent", 0.05f)
        };
    }
}
