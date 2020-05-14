using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class PlayerStats : MonoBehaviour
{
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
        CritDamage.BaseValue = 0;
        LifeOnHit.BaseValue = 0;
        SwordAttackModifier.BaseValue = 1.5f;
        CrossbowAttackModifier.BaseValue = 1f;
        PotionPotency.BaseValue = 0;
        MovementSpeed.BaseValue = 10;

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
    }
    public Item CheckforItems(int id)
    {
        return Loot.Find(item => item.id == id);
    }
    
    
    public void AddFlatModifier(CharacterStat statType, float statValue)
    {
        statType.AddModifier(new StatModifier(statValue, StatModType.Flat, this));
        UpdateItemInfo();
        player.UpdateStats();
    }
    public void AddPercentModifier(CharacterStat statType, float statValue)
    {
        statType.AddModifier(new StatModifier(statValue, StatModType.Percent, this));
        UpdateItemInfo();
        player.UpdateStats();
    }
    public void AddPercentMultModifier(CharacterStat statType, float statValue)
    {
        statType.AddModifier(new StatModifier(statValue, StatModType.PercentMult, this));
        UpdateItemInfo();
        player.UpdateStats();
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
            Strength.RemoveAllModifiersFromSource(this);
        }
    }
}
