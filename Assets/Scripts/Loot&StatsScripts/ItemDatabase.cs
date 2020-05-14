using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    //Stats
    public CharacterStat Health;
    public CharacterStat Strength;
    public CharacterStat Dexterity;
    public CharacterStat CritChance;
    public CharacterStat CritDamage;
    public CharacterStat LifeOnHit;
    public CharacterStat SwordAttackModifier;
    public CharacterStat CrossbowAttackModifier;
    public CharacterStat PotionPotency;
    public CharacterStat MovementSpeed;

    //Buffs
    public CharacterStat EnemiesVisibleInsideLight;
    public CharacterStat EnemiesVisibleOutsideLight;
    public CharacterStat IgnoreUnitCollision;
    public CharacterStat WalkAfterDodge;
    public CharacterStat IgnoreKnockback;
    public CharacterStat PotsIncreaseStr;
    public CharacterStat ArrowKnockback;
    public CharacterStat SwordProjectile;
    public CharacterStat SwordExecute;
    public CharacterStat TripleArrow;
    public CharacterStat RapidFire;
    public CharacterStat FireArrows;
    public CharacterStat SwordRangeIncreased;
    public CharacterStat SwordArcIncreased;
    public CharacterStat Power;
    public CharacterStat ShieldReflectsDmg;
    public CharacterStat NoSacrifice;
    public CharacterStat RueHPDmgOnHit;
    public CharacterStat PercentHpDmg;
    public CharacterStat ExtraLife;
    public CharacterStat DropGarantueed;

    //Debuffs
    public CharacterStat ShieldArm;
    public CharacterStat CanDodge;
    public CharacterStat HasEye;
    public CharacterStat HasSword;
    public CharacterStat HasCrossbow;
    public CharacterStat LessHP;
    public CharacterStat LessStr;
    public CharacterStat LessDex;
    public CharacterStat LessMS;
    public CharacterStat WeaknessCurse;
    public CharacterStat FrailtyCurse;



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
            new Item(0, "Common Loot", "Pale Scale", "A white scale from an unknown creature. Increases health.", "+ 25 Health.", Health, "flat", 25),
            new Item(1, "Common Loot", "Sharp Tooth", "A long and pointy tooth, it seems demonic. Increases strength.", "+ 1 Strength.", Strength, "flat", 1),
            new Item(2, "Common Loot", "Red String", "Hard to identify, but it is organic. Increases dexterity.", "+ 1 Dexterity.", Dexterity, "flat", 1),
            new Item(3, "Common Loot", "Shrunken Leech", "Shriveled, but still abnormally big. Increases life on hit.", "+ 1 Life on hit.", LifeOnHit, "flat", 1),
            new Item(4, "Common Loot", "Luminescent Black Mushroom", "A paradoxical and unnatural mushroom. Increases potion potency.", "+ 10 Potion Potency.", PotionPotency, "flat", 10),
            new Item(5, "Rare Loot", "Pale Skin",  "White and scaled skin which looks like snakeskin but has protrusions and holes which suggest otherwise. Increases health.", "+ 10 % Health.", Health, "percent", 0.1f),
            new Item(6, "Rare Loot", "Broken Horn", "A horn resembling a goat but seems predatory and evil… Seems like these pieces originate from the same creature. Increases attack damage.", "+ 10 % Strength.", Strength, "percent", 0.1f),
            new Item(7, "Rare Loot", "Red Guts", "Apparently intestines, but from what? Increases attack speed.", "+ 10 % Dexterity.", Dexterity, "percent", 0.1f),
            new Item(8, "Rare Loot", "Magnetic dust", "A hot magnetic powder clinging to sword blades; however, it does not attach to other metals. Increases sword damage.", "+ 25 % sword damage.", SwordAttackModifier, "percent", 0.25f),
            new Item(9, "Rare Loot", "Fetid wax", "Nauseating smell, but it is very smooth when applied to the crossbow barrel. Increases crossbow damage.", "+ 25 % crossbow damage.", CrossbowAttackModifier, "percent", 0.25f),
            new Item(10, "Rare Loot", "Pulsating something", "Mysteriously irresistible, invigorating when eaten. Increases movement speed.", "+ 5 % movement speed", MovementSpeed, "percent", 0.05f),
            new Item(11, "Rare Loot", "Stained Nail", "Feels like a plant, but hard as iron. Stained with dried liquids. Increases critical strike chance.", "+ 5 % Critical strike chance.", CritChance, "flat", 5),
            new Item(12, "Rare Loot", "Hair fetish", "Small occult looking doll. It seems to have the power to inflict great pain. Increases critical strike damage.", "+ 10 % Critical strike damage.", CritDamage, "flat", 10),
            new Item(13, "Legendary Loot", "Violet light particle", "Gives off no sensation of touch but can be grasped. Enemies can no longer hide in the light.", "All enemies are visible in the light.", EnemiesVisibleInsideLight, "flat", 1),
            new Item(14, "Legendary Loot",  "White light particle", "Cannot be grasped but feels strangely warm and solid. Enemies can no longer hide in the darkness.", "All enemies are visible in the darkness.", EnemiesVisibleOutsideLight, "flat", 1),
            new Item(15, "Legendary Loot", "Ghastly plasm", "Enters through skin. Parasite or spirit? Pass through enemies.", "Ignore unit collision.", IgnoreUnitCollision, "flat", 1),
            new Item(16, "Legendary Loot", "Intricate straps", "Confining ropes which coils around the chest. Increases poise.", "Enemies no longer knockback or interrupt with their attacks.", IgnoreKnockback, "flat", 1),
            new Item(17, "Legendary Loot", "Bundle of feathers", "The shafts are stained with blood, as if ripped out while something was still alive. Improves dodge.", "Dodging no longer locks out walking.", WalkAfterDodge, "flat", 1),
            new Item(18, "Legendary Loot", "Heavy pebble", "Unnaturally heavy compared to its size. Arrow force increased.", "Arrows knock back enemies.", ArrowKnockback, "flat", 1),
            new Item(19, "Legendary Loot", "Liquid crystal", "Impossibly liquid and solid stone. Attaches nicely to sword. Increases range.", "Sword range doubled.", SwordRangeIncreased, "flat", 1),
            new Item(20, "Legendary Loot", "Boiling blood", "Fizzling blood which absorbed itself to my sword without a trace. Increases sword arc.", "Sword arc doubled.", SwordArcIncreased, "flat", 1),
            new Item(21, "Legendary Loot", "Shadow mimic", "Small unintelligent creature, copies what it touches. Shoot more arrows.", "+2 additional arrows.", TripleArrow, "flat", 1),
            new Item(22, "Legendary Loot", "Possessed amulet", "Beautiful golden amulet, the jewel whispers constantly, but offers great power. Increases stats.", "Increases Health, Strength, Dexterity, Life on hit, Sword modifier and Crossbow modifier by 25 %.", Power, "flat", 0.25f),
            new Item(23, "Legendary Loot", "Bottomless vial", "Unending substance poured from a small vial, benefits potions. Potions increases Strength.", "Consuming potions increases Strength by 5", PotsIncreaseStr, "flat", 1),
            new Item(24, "Legendary Loot", "Second chance", "I have less regrets…", "Additional extra life.", ExtraLife, "flat", 1)
        };
    }
}
