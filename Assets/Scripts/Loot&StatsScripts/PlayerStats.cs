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
    public int ItemCount = 0;

    

    private void Start()
    {
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

        AddFlatModifier(Health, 0);
        AddFlatModifier(Strength, 0);
        AddFlatModifier(Dexterity, 0);
        AddFlatModifier(CritChance, 0);
        AddFlatModifier(CritDamage, 0);
        AddFlatModifier(LifeOnHit, 0);
        AddFlatModifier(SwordAttackModifier, 0);
        AddFlatModifier(CrossbowAttackModifier, 0);
        AddFlatModifier(PotionPotency, 0);
        AddFlatModifier(MovementSpeed, 0);
    }
    public void GiveItem(int id)
    {
        Item itemToAdd = ItemDatabase.GetItem(id);
        Loot.Add(itemToAdd);
        Debug.Log("Got " + itemToAdd.title + itemToAdd.description);
        ItemCount += 1;
        if (itemToAdd.modType == "flat")
        {
            AddFlatModifier(itemToAdd.statType, itemToAdd.statValue);
        }
        if (itemToAdd.modType == "percent")
        {
            AddPercentModifier(itemToAdd.statType, itemToAdd.statValue);
        }
        if (itemToAdd.modType == "multpercent")
        {
            AddPercentMultModifier(itemToAdd.statType, itemToAdd.statValue);
        }
        Item itemCheck = CheckforItems(id);
    }
    private void UpdateItemInfo()
    {
        _ = Health.Value;
        _ = Strength.Value;
        _ = Dexterity.Value;
        _ = CritChance.Value;
        _ = CritDamage.Value;
        _ = LifeOnHit.Value;
        _ = SwordAttackModifier.Value;
        _ = CrossbowAttackModifier.Value;
        _ = PotionPotency.Value;
        _ = MovementSpeed.Value;

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
        _ = ShieldReflectsDmg.Value;
        _ = NoSacrifice.Value;
        _ = RueHPDmgOnHit.Value;
        _ = PercentHpDmg.Value;
        _ = ExtraLife.Value;
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
        return Loot.Find(item => item.id == id);
    }
    
    
    public void AddFlatModifier(CharacterStat statType, float statValue)
    {
        statType.AddModifier(new StatModifier(statValue, StatModType.Flat, this));
        UpdateItemInfo();
    }
    public void AddPercentModifier(CharacterStat statType, float statValue)
    {
        statType.AddModifier(new StatModifier(statValue, StatModType.Percent, this));
        UpdateItemInfo();
    }
    public void AddPercentMultModifier(CharacterStat statType, float statValue)
    {
        statType.AddModifier(new StatModifier(statValue, StatModType.PercentMult, this));
        UpdateItemInfo();
    }

    public void RemoveItem(int id)
    {
        Item item = CheckforItems(id);
        if (item != null)
        {
            RemoveModifier();
            Loot.Remove(item);
            ItemCount -= 1;
            player.UpdateStats();
        }
    }
    public void RemoveModifier()
    {
        {
            
        }
    }
}
