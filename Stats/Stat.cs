using System.Collections.Generic;
using UnityEngine;


public enum StatIdentifierEnum
{
    Str,
    Agi,
    Vit,
    Crit,
    Spec,
    Int,
    Dex,
    Atk,
    Matk,
    Hit,
    CritChance,
    Def,
    SoftDef,
    MDef,
    SoftMDef,
    Flee,
    Aspd,
    SPoints,
    MaxMana,
    ManaRegen,
    MaxHealth,
    HealthRegen,
    WeaponAtk,
    WeaponRange
}
[System.Serializable]
public class Stat
{
    [SerializeField]
    private List<float> modifiers = new List<float>();
    private List<float> percentagemodifiers = new List<float>();
    public float baseValue = 0;
    public float percentagebaseValue;
    public StatIdentifierEnum thisStatIdentifier_Enum { get; private set; }
    // ... existing fields and methods ...

    public Stat(StatIdentifierEnum specificIdentifier)
    {
        thisStatIdentifier_Enum = specificIdentifier;
    }
    public float GetValue()
    {
        float finalValue = baseValue;
        float finalPercentageValue = percentagebaseValue;
        modifiers.ForEach(x => finalValue += x);
        percentagemodifiers.ForEach(y => finalPercentageValue += y);
        finalValue = finalValue * (1 + finalPercentageValue / 100);
        return finalValue;
    }

    public void AddFlatModifier(float modifier)
    {
        if (modifier != 0)
            modifiers.Add(modifier);
    }
    public void RemoveFlatModifier(float modifier)
    {
        if (modifier != 0)
            modifiers.Remove(modifier);
    }
    public void AddPercentageModifier(float percentagemodifier)
    {
        if (percentagemodifier != 0)
            percentagemodifiers.Add(percentagemodifier);
    }
    public void RemovePercentageModifier(float percentagemodifier)
    {
        if (percentagemodifier != 0)
            percentagemodifiers.Remove(percentagemodifier);
    }
}
