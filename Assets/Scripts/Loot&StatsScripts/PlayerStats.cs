using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class PlayerStats : MonoBehaviour
{
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


    public List<Item> Loot = new List<Item>();
    public ItemDatabase ItemDatabase;
    public Player player => GetComponent<Player>();

    private StatModifier flat;
    private StatModifier percent;
    private StatModifier mult;

    public bool power = false;
    public bool lifeonhit = false;
    public bool extralife = false;

    private void Start()
    {
        //Sets basevalues on stats, and updates the rue script to use these stats.
        Health.BaseValue = 50;
        Strength.BaseValue = 10;
        Dexterity.BaseValue = 10;
        CritChance.BaseValue = 0;
        CritDamage.BaseValue = 200;
        LifeOnHit.BaseValue = 0;
        SwordAttackModifier.BaseValue = 1.5f;
        CrossbowAttackModifier.BaseValue = 1f;
        PotionPotency.BaseValue = 0;
        MovementSpeed.BaseValue = 10;

        EnemiesVisibleInsideLight.BaseValue = 0;
        EnemiesVisibleOutsideLight.BaseValue = 0;
        IgnoreUnitCollision.BaseValue = 0;
        WalkAfterDodge.BaseValue = 0;
        IgnoreKnockback.BaseValue = 0;
        PotsIncreaseStr.BaseValue = 0;
        ArrowKnockback.BaseValue = 0;
        SwordProjectile.BaseValue = 0;
        SwordExecute.BaseValue = 0;
        TripleArrow.BaseValue = 0;
        RapidFire.BaseValue = 0;
        FireArrows.BaseValue = 0;
        SwordRangeIncreased.BaseValue = 0;
        SwordArcIncreased.BaseValue = 0;
        Power.BaseValue = 0;
        ShieldReflectsDmg.BaseValue = 0;
        NoSacrifice.BaseValue = 0;
        RueHPDmgOnHit.BaseValue = 0;
        IncreasedLifeOnHit.BaseValue = 0;
        PercentHpDmg.BaseValue = 0;
        ExtraLife.BaseValue = 0;
        DropGarantueed.BaseValue = 0;

        ShieldArm.BaseValue = 0;
        CanDodge.BaseValue = 0;
        HasEye.BaseValue = 0;
        HasSword.BaseValue = 0;
        HasCrossbow.BaseValue = 0;
        LessHP.BaseValue = 0;
        LessStr.BaseValue = 0;
        LessDex.BaseValue = 0;
        LessMS.BaseValue = 0;
        WeaknessCurse.BaseValue = 0;
        FrailtyCurse.BaseValue = 0;

        player.UpdateStats();

        UpdateStatsInfo();

    }
    public void GiveItem(int id) //the method used in other scripts to transfer the correct item from the ItemDatabase to this script. This is called in Accept Loot script.
    {
        //Debug.Log("GiveItem() started");
        Item itemToAdd = ItemDatabase.GetItem(id);
        bool AlreadyinInventory = false;
        foreach (Item item in Loot) //Checks if already in inventory, and if it is, it adds a higher number to collection instead of copies within the list.
        {
            //Debug.Log((i++) + " counts in foreach loop");
            if (item.id == itemToAdd.id)
            {
                item.collection += 1;
                AlreadyinInventory = true;
                Item itemCheck = CheckforItems(id);

                if (itemToAdd.modType == "flat") //adds additive flat number increases / decreases. 10 + 10 = 20
                {
                    itemToAdd.statType.RemoveModifier(flat);
                    AddFlatModifier(itemToAdd.statType, itemToAdd.statValue * itemToAdd.collection);
                }
                if (itemToAdd.modType == "percent") //adds percentage increases / decreases. 10 + 10% = 11
                {
                    itemToAdd.statType.RemoveModifier(percent);
                    AddPercentModifier(itemToAdd.statType, itemToAdd.statValue * itemToAdd.collection);
                }
                if (itemToAdd.modType == "multpercent") //adds multiplicative percent increases. (10 + 10%) 11 + (10% + 10%) 11% = 22.21
                {
                    itemToAdd.statType.RemoveModifier(mult);
                    AddPercentMultModifier(itemToAdd.statType, itemToAdd.statValue * itemToAdd.collection);
                }
            }
        }
        if (!AlreadyinInventory)
        {
            Loot.Add(itemToAdd);
            Item itemCheck = CheckforItems(id); //checks if item id is in list.

            if (itemToAdd.modType == "flat")
            {
                itemToAdd.statType.RemoveModifier(flat);
                AddFlatModifier(itemToAdd.statType, itemToAdd.statValue * itemToAdd.collection);
            }
            if (itemToAdd.modType == "percent")
            {
                itemToAdd.statType.RemoveModifier(percent);
                if (itemToAdd.statType == Health &&  LessHP.Value > 0)
                {
                    AddPercentModifier(itemToAdd.statType, (itemToAdd.statValue * itemToAdd.collection) - 0.25f);
                }
                if (itemToAdd.statType == Strength && LessStr.Value > 0)
                {
                    AddPercentModifier(itemToAdd.statType, (itemToAdd.statValue * itemToAdd.collection) - 0.25f);
                }
                if (itemToAdd.statType == Dexterity && LessDex.Value > 0)
                {
                    AddPercentModifier(itemToAdd.statType, (itemToAdd.statValue * itemToAdd.collection) - 0.25f);
                }
                if (itemToAdd.statType == MovementSpeed && LessMS.Value > 0)
                {
                    AddPercentModifier(itemToAdd.statType, (itemToAdd.statValue * itemToAdd.collection) - 0.25f);
                }
                else
                {
                    AddPercentModifier(itemToAdd.statType, itemToAdd.statValue * itemToAdd.collection);
                }
            }
            if (itemToAdd.modType == "multpercent")
            {
                itemToAdd.statType.RemoveModifier(mult);
                AddPercentMultModifier(itemToAdd.statType, itemToAdd.statValue * itemToAdd.collection);
            }
        }
        
    }
    private void UpdateStatsInfo()
    {
        //Debug.Log("Updated stats");
        _ = Health.Value;
        _ = Strength.Value;
        _ = Dexterity.Value;
        _ = CritChance.Value;

        if (CritChance.Value > 100)
        {
            RemoveAllModifiers(CritChance);
            if (CritChance.Value == CritChance.BaseValue) //if value above max, remove modifiers and set to max.
            {
                AddFlatModifier(CritChance, 100);
                return;
            }

        }

        _ = CritDamage.Value;
        _ = LifeOnHit.Value;
        _ = SwordAttackModifier.Value;
        _ = CrossbowAttackModifier.Value;
        _ = PotionPotency.Value;
        _ = MovementSpeed.Value;

        if (MovementSpeed.Value > 15)
        {
            RemoveAllModifiers(MovementSpeed);
            if (MovementSpeed.Value == MovementSpeed.BaseValue)
            {
                AddFlatModifier(MovementSpeed, 15);
                return;
            }
        }

        _ = EnemiesVisibleInsideLight.Value;
        _ = EnemiesVisibleOutsideLight.Value;
        _ = IgnoreUnitCollision.Value;
        _ = WalkAfterDodge.Value;
        _ = IgnoreKnockback.Value;
        _ = PotsIncreaseStr.Value;
        _ = ArrowKnockback.Value;
        _ = SwordProjectile.Value;
        _ = SwordExecute.Value;
        _ = TripleArrow.Value;
        _ = RapidFire.Value;
        _ = FireArrows.Value;
        _ = SwordRangeIncreased.Value;
        _ = SwordArcIncreased.Value;
        _ = Power.Value;
        if (Power.Value > 0 && !power)
        {
            power = true;
            AddPercentMultModifier(Health, 0.25f);
            if (FrailtyCurse.Value > 0)
            {
                AddPercentMultModifier(Health, -0.25f);
            }
            AddPercentMultModifier(Strength, 0.25f);
            if (WeaknessCurse.Value > 0)
            {
                AddPercentMultModifier(Strength, -0.25f);
            }
            AddPercentMultModifier(Dexterity, 0.25f);
            AddPercentMultModifier(LifeOnHit, 0.25f);
            AddPercentMultModifier(SwordAttackModifier, 0.25f);
            AddPercentMultModifier(CrossbowAttackModifier, 0.25f);
        }
        if (Power.Value <= 0 && power)
        {
            power = false;
            Health.RemoveModifier(mult);
            if (FrailtyCurse.Value > 0)
            {
                AddPercentMultModifier(Health, -0.25f);
            }
            Strength.RemoveModifier(mult);
            if (WeaknessCurse.Value > 0)
            {
                AddPercentMultModifier(Strength, -0.25f);
            }
            Dexterity.RemoveModifier(mult);
            LifeOnHit.RemoveModifier(mult);
            SwordArcIncreased.RemoveModifier(mult);
            CrossbowAttackModifier.RemoveModifier(mult);

        }
        _ = ShieldReflectsDmg.Value;
        _ = NoSacrifice.Value;
        _ = RueHPDmgOnHit.Value;
        _ = IncreasedLifeOnHit;
        if (IncreasedLifeOnHit.Value > 0 && !lifeonhit)
        {
            lifeonhit = true;
            AddPercentModifier(LifeOnHit, 3);
        }
        if (IncreasedLifeOnHit.Value <= 0 && lifeonhit)
        {
            lifeonhit = false;
            LifeOnHit.RemoveModifier(percent);
        }
        _ = PercentHpDmg.Value;
        _ = ExtraLife.Value;
        if (ExtraLife.Value > 0 && !extralife)
        {
            extralife = true;
            player.extraLives += 1;
        }
        if (ExtraLife.Value <= 0 && extralife)
        {
            extralife = false;
            player.extraLives -= 1;
        }
        _ = DropGarantueed.Value;

        _ = ShieldArm.Value;
        _ = CanDodge.Value;
        _ = HasEye.Value;
        _ = HasSword.Value;
        _ = HasCrossbow.Value;
        _ = LessHP.Value;
        _ = LessStr.Value;
        _ = LessDex.Value;
        _ = LessMS.Value;
        _ = WeaknessCurse.Value;
        _ = FrailtyCurse.Value;

        player.UpdateStats();
    }
    public Item CheckforItems(int id)
    {
        return Loot.Find(item => item.id == id); //finds and returns id.
    }
    
    public void SacrificeModifier(Item item)
    {
        if (item.modType == "flat")
        {
            item.statType.RemoveModifier(flat);
            AddFlatModifier(item.statType, item.statValue * item.collection);
            UpdateStatsInfo();
        }
        if (item.modType == "percent")
        {
            item.statType.RemoveModifier(percent);
            if (item.statType == Health && LessHP.Value > 0)
            {
                AddPercentModifier(item.statType, (item.statValue * item.collection) - 0.25f);
            }
            if (item.statType == Strength && LessStr.Value > 0)
            {
                AddPercentModifier(item.statType, (item.statValue * item.collection) - 0.25f);
            }
            if (item.statType == Dexterity && LessDex.Value > 0)
            {
                AddPercentModifier(item.statType, (item.statValue * item.collection) - 0.25f);
            }
            if (item.statType == MovementSpeed && LessMS.Value > 0)
            {
                AddPercentModifier(item.statType, (item.statValue * item.collection) - 0.25f);
            }
            else
            {
                AddPercentModifier(item.statType, item.statValue * item.collection);
            }
            UpdateStatsInfo();
        }
        if (item.modType == "mult")
        {
            item.statType.RemoveModifier(mult);
            AddPercentMultModifier(item.statType, item.statValue * item.collection);
            UpdateStatsInfo();
        }
    }
    
    public void AddFlatModifier(CharacterStat statType, float statValue)
    {
        flat = new StatModifier(statValue, StatModType.Flat, this); //input desired stat modifier and stat will be modified and updated.
        statType.AddModifier(flat);
        UpdateStatsInfo();
    }
    public void AddPercentModifier(CharacterStat statType, float statValue)
    {
        percent = new StatModifier(statValue, StatModType.Percent, this);
        statType.AddModifier(percent);
        UpdateStatsInfo();
    }
    public void AddPercentMultModifier(CharacterStat statType, float statValue)
    {
        mult = new StatModifier(statValue, StatModType.PercentMult, this);
        statType.AddModifier(mult);
        UpdateStatsInfo();
    }

    public void RemoveItem(int id)
    {
        Item item = CheckforItems(id);
        if (item != null)
        {
            Loot.Remove(item); //removes item from inventory
        }
    }
    public void RemoveAllModifiers(CharacterStat statType) //removes modifiers from items. (All at once at the moment.)
    {
        {
            statType.RemoveModifier(flat);
            statType.RemoveModifier(percent);
            statType.RemoveModifier(mult);
            UpdateStatsInfo();
        }
    }
}
