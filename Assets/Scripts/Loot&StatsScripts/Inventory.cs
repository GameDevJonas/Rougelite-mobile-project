using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Inventory : MonoBehaviour
{
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
    public List<Item> Loot = new List<Item>();
    public ItemDatabase ItemDatabase;
    public PlayerMovement player => GetComponent<PlayerMovement>();
    public int ItemCount = 0;

    

    private void Start()
    {
        Health.BaseValue = 100;
        HealthPercent.BaseValue = 1f;
        Strength.BaseValue = 10;
        StrengthPercent.BaseValue = 1f;
        Dexterity.BaseValue = 10;
        CritChance.BaseValue = 0;
        CritDamage.BaseValue = 0;
        LifeOnHit.BaseValue = 0;
        LifeOnHitPercent.BaseValue = 1f;
        SwordAttackModifier.BaseValue = 1.5f;
        CrossbowAttackModifier.BaseValue = 1f;
        PotionPotency.BaseValue = 0;
        MovementSpeed.BaseValue = 5f;

        player.UpdateStats();

        AddFlatModifier(Health, 0);
        AddFlatModifier(HealthPercent, 0);
        AddFlatModifier(Strength, 0);
        AddFlatModifier(StrengthPercent, 0);
        AddFlatModifier(Dexterity, 0);
        AddFlatModifier(CritChance, 0);
        AddFlatModifier(CritDamage, 0);
        AddFlatModifier(LifeOnHit, 0);
        AddFlatModifier(LifeOnHitPercent, 0);
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
        AddFlatModifier(itemToAdd.statType, itemToAdd.statValue);
        Item itemCheck = CheckforItems(id);
    }
    private void UpdateItemInfo()
    {
        Debug.Log("I have " + ItemCount + " items and " + Health.Value + "Health and " + Strength.Value + "Strength");
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
    public void AddPercentAddModifier(CharacterStat statType, float statValue)
    {
        statType.AddModifier(new StatModifier(statValue, StatModType.PercentAdd, this));
        player.UpdateStats();
    }
    public void AddFlatPercentMultModifier(CharacterStat statType, float statValue)
    {
        statType.AddModifier(new StatModifier(statValue, StatModType.PercentMult, this));
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
