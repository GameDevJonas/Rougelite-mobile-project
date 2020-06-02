﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    public CharacterStat IncreasedLifeOnHit;
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
        LifeOnHit = playerstats.LifeOnHit;
        SwordAttackModifier = playerstats.SwordAttackModifier;
        CrossbowAttackModifier = playerstats.CrossbowAttackModifier;
        PotionPotency = playerstats.PotionPotency;
        MovementSpeed = playerstats.MovementSpeed;

        EnemiesVisibleInsideLight = playerstats.EnemiesVisibleInsideLight;
        EnemiesVisibleOutsideLight = playerstats.EnemiesVisibleOutsideLight;
        IgnoreUnitCollision = playerstats.IgnoreUnitCollision;
        WalkAfterDodge = playerstats.WalkAfterDodge;
        IgnoreKnockback = playerstats.IgnoreKnockback;
        PotsIncreaseStr = playerstats.PotsIncreaseStr;
        ArrowKnockback = playerstats.ArrowKnockback;
        SwordProjectile = playerstats.SwordProjectile;
        SwordExecute = playerstats.SwordExecute;
        TripleArrow = playerstats.TripleArrow;
        RapidFire = playerstats.RapidFire;
        FireArrows = playerstats.FireArrows;
        SwordRangeIncreased = playerstats.SwordRangeIncreased;
        SwordArcIncreased = playerstats.SwordArcIncreased;
        Power = playerstats.Power;
        ShieldReflectsDmg = playerstats.ShieldReflectsDmg;
        NoSacrifice = playerstats.NoSacrifice;
        RueHPDmgOnHit = playerstats.RueHPDmgOnHit;
        IncreasedLifeOnHit = playerstats.IncreasedLifeOnHit;
        PercentHpDmg = playerstats.PercentHpDmg;
        ExtraLife = playerstats.ExtraLife;
        DropGarantueed = playerstats.DropGarantueed;

        ShieldArm = playerstats.ShieldArm;
        CanDodge = playerstats.CanDodge;
        HasEye = playerstats.HasEye;
        HasSword = playerstats.HasSword;
        HasCrossbow = playerstats.HasCrossbow;
        LessHP = playerstats.LessHP;
        LessStr = playerstats.LessStr;
        LessDex = playerstats.LessDex;
        LessMS = playerstats.LessMS;
        WeaknessCurse = playerstats.WeaknessCurse;
        FrailtyCurse = playerstats.FrailtyCurse;

        BuildDatabase();
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id); //find item by ID and give to the script using this function
    }

    void BuildDatabase()
    {
        items = new List<Item>()
        {
            new Item(0, "Common Loot", "Pale scale", "Pale scales", "A white scale from an unknown creature. Increases health.", "+ 25 Health.", Health, "Health", "flat", 25, 1, 1),
            new Item(1, "Common Loot", "Sharp tooth", "Sharp teeth", "A long and pointy tooth, it seems demonic. Increases strength.", "+ 1 Strength.", Strength, "Strenght", "flat", 1, 1, 1),
            new Item(2, "Common Loot", "Red string", "Red strings", "Hard to identify, but it is organic. Increases dexterity.", "+ 1 Dexterity.", Dexterity, "Dexterity", "flat", 1, 1, 1),
            new Item(3, "Common Loot", "Shrunken leech", "Shrunken leeches", "Shriveled, but still abnormally big. Increases life on hit.", "+ 1 Life on hit.", LifeOnHit, "Life on hit", "flat", 1, 1, 1),
            new Item(4, "Common Loot", "Luminescent black mushroom", "Luminescent black mushrooms", "A paradoxical and unnatural mushroom. Increases potion potency.", "+ 10 Potion Potency.", PotionPotency, "Potion Potency", "flat", 10, 1, 1),
            new Item(5, "Rare Loot", "Pale skin", "Pale skins",  "White and scaled skin which looks like snakeskin but has protrusions and holes which suggest otherwise. Increases health.", "+ 10 % Health.", Health, "Health", "percent", 0.1f, 1, 1),
            new Item(6, "Rare Loot", "Broken horn", "Broken horns", "A horn resembling a goat but seems predatory and evil… Seems like these pieces originate from the same creature. Increases attack damage.", "+ 10 % Strength.", Strength, "Strenght", "percent", 0.1f, 1, 1),
            new Item(7, "Rare Loot", "Red guts", null, "Apparently intestines, but from what? Increases attack speed.", "+ 10 % Dexterity.", Dexterity, "Dexterity", "percent", 0.1f, 1, 1),
            new Item(8, "Rare Loot", "Magnetic dust", null, "A hot magnetic powder clinging to sword blades; however, it does not attach to other metals. Increases sword damage.", "+ 25 % Sword damage.", SwordAttackModifier, "Sword damage", "percent", 0.25f, 1, 1),
            new Item(9, "Rare Loot", "Fetid wax", null, "Nauseating smell, but it is very smooth when applied to the crossbow barrel. Increases crossbow damage.", "+ 25 % Crossbow damage.", CrossbowAttackModifier, "Crossbow damage", "percent", 0.25f, 1, 1),
            new Item(10, "Rare Loot", "Pulsating something", "Pulsating somethings", "Mysteriously irresistible, invigorating when eaten. Increases movement speed.", "+ 5 % Movement speed", MovementSpeed, "Movement speed", "percent", 0.05f, 1, 1),
            new Item(11, "Rare Loot", "Stained nail", "Stained nails", "Feels like a plant, but hard as iron. Stained with dried liquids. Increases critical strike chance.", "+ 10 % Critical strike chance.", CritChance, "Critical strike chance", "flat", 10, 1, 1),
            new Item(12, "Rare Loot", "Hair fetish", "Hair fetishes", "Small occult looking doll. It seems to have the power to inflict great pain. Increases critical strike damage.", "+ 25 % Critical strike damage.", CritDamage, "Critical strike damage", "flat", 25, 1, 1),
            new Item(13, "Legendary Loot", "Violet light particle", null, "Gives off no sensation of touch but can be grasped. Enemies can no longer hide in the light.", "All enemies are visible in the light.", EnemiesVisibleInsideLight, null, "flat", 1, 1, 1),
            new Item(14, "Legendary Loot",  "White light particle", null, "Cannot be grasped but feels strangely warm and solid. Enemies can no longer hide in the darkness.", "All enemies are visible in the darkness.", EnemiesVisibleOutsideLight, null, "flat", 1, 1, 1),
            new Item(15, "Legendary Loot", "Ghastly plasm", "Ghastly plasms", "Enters through skin. Parasite or spirit? Pass through enemies.", "Ignore unit collision.", IgnoreUnitCollision, null, "flat", 1, 1, 1),
            new Item(16, "Legendary Loot", "Intricate straps", null, "Confining ropes which coils around the chest. Increases poise.", "Enemies no longer knockback or interrupt with their attacks.", IgnoreKnockback, null, "flat", 1, 1, 1),
            new Item(17, "Legendary Loot", "Bundle of feathers", null, "The shafts are stained with blood, as if ripped out while something was still alive. Improves dodge.", "Dodging no longer locks out walking.", WalkAfterDodge, null, "flat", 1, 1, 1),
            new Item(18, "Legendary Loot", "Heavy pebble", null, "Unnaturally heavy compared to its size. Arrow force increased.", "Arrows knock back enemies.", ArrowKnockback, null, "flat", 1, 1, 1),
            new Item(19, "Legendary Loot", "Liquid crystal", null, "Impossibly liquid and solid stone. Attaches nicely to sword. Increases range.", "Sword range doubled.", SwordRangeIncreased, null, "flat", 1, 1, 1),
            new Item(20, "Legendary Loot", "Boiling blood", null, "Fizzling blood which absorbed itself to my sword without a trace. Increases sword arc.", "Sword arc doubled.", SwordArcIncreased, null, "flat", 1, 1, 1),
            new Item(21, "Legendary Loot", "Shadow mimic", null, "Small unintelligent creature, copies what it touches. Shoot more arrows.", "+2 additional arrows.", TripleArrow, null, "flat", 1, 1, 1),
            new Item(22, "Legendary Loot", "Possessed amulet", null, "Beautiful golden amulet, the jewel whispers constantly, but offers great power. Increases stats.", "Increases Health, Strength, Dexterity, Life on hit, Sword modifier and Crossbow modifier by 25 %.", Power, null, "flat", 1, 1, 1),
            new Item(23, "Legendary Loot", "Bottomless vial", null, "Unending substance poured from a small vial, benefits potions. Potions increases Strength.", "Consuming potions increases Strength by 5", PotsIncreaseStr, null, "flat", 1, 1, 1),
            new Item(24, "Legendary Loot", "Second chance", null, "I have less regrets…", "Additional extra life.", ExtraLife, null, "flat", 1, 1, 1),
            new Item(25, "Ancient Loot", "Nihilistic essence", null, "Children of chance and suffering, This void of meaninglessness, All pains endured, Are pains to be inflicted.", "Shield reflects damage.", ShieldReflectsDmg, null, "flat", 1, 1, 1),
            new Item(26, "Ancient Loot", "Futile avarice", null, "For those who have all, All will be given, From those who have nothing, All will be taken.", "Loot drop chance is 100 %, potions has a 50 % chance to additionally drop.", DropGarantueed, null, "flat", 1, 1, 1),
            new Item(27, "Ancient Loot", "Madman’s insight", null, "Forbidden fruits of knowledge, To know good and evil, Know you can be hurt, Know you can hurt others.", "Deal 10% of your maximum health as damage.", RueHPDmgOnHit, null, "flat", 1, 1, 1),
            new Item(28, "Ancient Loot", "Desperate will", null, "Suffering rooted in desire, To steal life from others, Desperation is, What desperation does.", "Life on hit is increased by 300%.", IncreasedLifeOnHit, null, "flat", 1, 1, 1),
            new Item(29, "Ancient Loot", "Infernal flame", null, "Red and hot demon soul, Ancient past unresolved, A bitter flame, Burns through all.", "Fire arrows which pierce through enemies, arrow damage doubled.", FireArrows, null, "flat", 1, 1, 1),
            new Item(30, "Ancient Loot", "Hell frost", null, "Black and frozen, Cold injustice, The chill of spite, Spite passed on.", "Sword swings shoots a projectile and deals damage twice.", SwordProjectile, null, "flat", 1, 1, 1),
            new Item(31, "Ancient Loot", "Surfaced wrath", null, "When you raise the wall for long, To contain the turmoil of your soul. We will take hold and violently erupt, Your sorrows made wrath and violence inflicted.", "Gains rapid fire arrows, damage is increased by Dexterity.", RapidFire, null, "flat", 1, 1, 1),
            new Item(32, "Ancient Loot", "Hatred fragment", null, "Fundamentally flawed, Corruption beyond repair. Why mend what is broken, Instead of swift destruction?", "Sword attacks execute enemies below 20% health.", SwordExecute, null, "flat", 1, 1, 1),
            new Item(33, "Ancient Loot", "Guilt of Belial", null, "I see what is broken and breaks them further, I torment them with their past and torment them with the future. I trap their lives in a prison of time with bars of guilt.", "Boss doors and death no longer require sacrifices", NoSacrifice, null, "flat", 1, 1, 1),
            new Item(34, "Ancient Loot", "Wickedness of Belial", null, "I whisper sweet promises and sweet release of sin, I say where all fault lies, not with them but with others. I inspire their souls with wickedness and claim justice for their victimhood.", "Critical strikes deal equal to 2% of enemies’ maximum health.", PercentHpDmg, null, "flat", 1, 1, 1)
        };
    }
}
