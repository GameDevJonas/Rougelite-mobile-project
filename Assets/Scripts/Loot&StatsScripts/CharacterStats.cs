using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;

[Serializable]
public class CharacterStat
{
    protected bool isDirty = true;
    public float _value;
    public float BaseValue;
    protected float lastBaseValue = float.MinValue;
    protected readonly List<StatModifier> statModifiers;
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;

   
    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b) //sorts order so flat, percent and multiplicative values are calculated correctly
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0; // if (a.Order == b.Order)
    }
    public CharacterStat()
    {
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly(); //read only, so they're not tampered with. It poops itself if modified after a bunch of calculations.
    }

    public CharacterStat(float baseValue) : this()
    {
        BaseValue = baseValue; //sets base value either in scripts or inspector.
    }
    public virtual float Value
    {
        get
        {
            if (isDirty || lastBaseValue != BaseValue) //only updates when changed, instead of constantly
            {
                lastBaseValue = BaseValue;
                _value = CalculateFinalValue(); 
                isDirty = false;
            }
            return _value;
        }
    }

    public virtual void AddModifier(StatModifier mod) //method to add modifiers in other scripts.
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }

    public virtual bool RemoveModifier(StatModifier mod) //method to remove modifiers in other scripts.
    {
        if (statModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }
    public virtual bool RemoveAllModifiersFromSource(object source) //method to remove specific item modifiers in other scripts.
    {
        bool didRemove = false;

        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentAdd = 0; // This will hold the sum of our "PercentAdd" modifiers

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];

            if (mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.Percent) // When we encounter a "PercentAdd" modifier
            {
                sumPercentAdd += mod.Value; // Start adding together all modifiers of this type

                // If we're at the end of the list OR the next modifer isn't of this type
                if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.Percent)
                {
                    finalValue *= 1 + sumPercentAdd; // Multiply the sum with the "finalValue", like we do for "PercentMult" modifiers
                    sumPercentAdd = 0; // Reset the sum back to 0
                }
            }
            else if (mod.Type == StatModType.PercentMult) // Percent renamed to PercentMult
            {
                finalValue *= 1 + mod.Value;
            }
        }

        return (float)Math.Round(finalValue, 4); //rounds floats to be more comprehensible.
    }
}

