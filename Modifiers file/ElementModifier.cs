using System;
using System.Collections.Generic;
using UnityEngine;
public enum Element
{
    Neutral,
    Water,
    Earth,
    Fire,
    Wind,
    Holy,
    Shadow,
    Ghost,
    Undead
}
public static class ElementModifier
{
    private static float[,] damageMultipliers = new float[Enum.GetNames(typeof(Element)).Length, Enum.GetNames(typeof(Element)).Length];

    private static void AddDamageMultiplier(Element attackingElement, Element defendingElement, float multiplier)
    {
        damageMultipliers[(int)attackingElement, (int)defendingElement] = multiplier;
    }
    
    public static float GetDamageMultiplier(Element attackingElement, Element defendingElement)
    {
        return damageMultipliers[(int)attackingElement, (int)defendingElement];
    }
    static ElementModifier()
    {
        // Initialize the damage multipliers for each element combination
        InitializeDamageMultipliers();
    }

    private static void InitializeDamageMultipliers()
    {
        // Neutral
        AddDamageMultiplier(Element.Neutral, Element.Neutral, 1f);
        AddDamageMultiplier(Element.Neutral, Element.Water, 1f);
        AddDamageMultiplier(Element.Neutral, Element.Earth, 1f);
        AddDamageMultiplier(Element.Neutral, Element.Fire, 1f);
        AddDamageMultiplier(Element.Neutral, Element.Wind, 1f);
        AddDamageMultiplier(Element.Neutral, Element.Holy, 1f);
        AddDamageMultiplier(Element.Neutral, Element.Shadow, 1f);
        AddDamageMultiplier(Element.Neutral, Element.Ghost, 0.5f);
        AddDamageMultiplier(Element.Neutral, Element.Undead, 1f);

        // Water
        AddDamageMultiplier(Element.Water, Element.Neutral, 1f);
        AddDamageMultiplier(Element.Water, Element.Water, 0.5f);
        AddDamageMultiplier(Element.Water, Element.Earth, 1.25f);
        AddDamageMultiplier(Element.Water, Element.Fire, 1.5f);
        AddDamageMultiplier(Element.Water, Element.Wind, 0.75f);
        AddDamageMultiplier(Element.Water, Element.Holy, 0.75f);
        AddDamageMultiplier(Element.Water, Element.Shadow, 0.75f);
        AddDamageMultiplier(Element.Water, Element.Ghost, 1f);
        AddDamageMultiplier(Element.Water, Element.Undead, 1f);

        // Earth
        AddDamageMultiplier(Element.Earth, Element.Neutral, 1f);
        AddDamageMultiplier(Element.Earth, Element.Water, 1f);
        AddDamageMultiplier(Element.Earth, Element.Earth, 0.5f);
        AddDamageMultiplier(Element.Earth, Element.Fire, 0.75f);
        AddDamageMultiplier(Element.Earth, Element.Wind, 1.5f);
        AddDamageMultiplier(Element.Earth, Element.Holy, 0.75f);
        AddDamageMultiplier(Element.Earth, Element.Shadow, 1.25f);
        AddDamageMultiplier(Element.Earth, Element.Ghost, 1f);
        AddDamageMultiplier(Element.Earth, Element.Undead, 1f);
        // Add damage multipliers for Earth element

        // Fire
        AddDamageMultiplier(Element.Fire, Element.Neutral, 1f);
        AddDamageMultiplier(Element.Fire, Element.Water, 0.5f);
        AddDamageMultiplier(Element.Fire, Element.Earth, 1.5f);
        AddDamageMultiplier(Element.Fire, Element.Fire, 0.5f);
        AddDamageMultiplier(Element.Fire, Element.Wind, 1f);
        AddDamageMultiplier(Element.Fire, Element.Holy, 0.75f);
        AddDamageMultiplier(Element.Fire, Element.Shadow, 1f);
        AddDamageMultiplier(Element.Fire, Element.Ghost, 1f);
        AddDamageMultiplier(Element.Fire, Element.Undead, 1.5f);
        // Add damage multipliers for Fire element

        // Wind
        AddDamageMultiplier(Element.Wind, Element.Neutral, 1f);
        AddDamageMultiplier(Element.Wind, Element.Water, 1.5f);
        AddDamageMultiplier(Element.Wind, Element.Earth, 0.5f);
        AddDamageMultiplier(Element.Wind, Element.Fire, 1f);
        AddDamageMultiplier(Element.Wind, Element.Wind, 0.5f);
        AddDamageMultiplier(Element.Wind, Element.Holy, 0.75f);
        AddDamageMultiplier(Element.Wind, Element.Shadow, 1.25f);
        AddDamageMultiplier(Element.Wind, Element.Ghost, 1f);
        AddDamageMultiplier(Element.Wind, Element.Undead, 1f);
        // Add damage multipliers for Wind element

        // Holy
        AddDamageMultiplier(Element.Holy, Element.Neutral, -0.75f);
        AddDamageMultiplier(Element.Holy, Element.Water, -1f);
        AddDamageMultiplier(Element.Holy, Element.Earth, -1f);
        AddDamageMultiplier(Element.Holy, Element.Fire, -1f);
        AddDamageMultiplier(Element.Holy, Element.Wind, -1f);
        AddDamageMultiplier(Element.Holy, Element.Holy, -2f);
        AddDamageMultiplier(Element.Holy, Element.Shadow, 1f);
        AddDamageMultiplier(Element.Holy, Element.Ghost, 1f);
        AddDamageMultiplier(Element.Holy, Element.Undead, 2f);
        // Add damage multipliers for Holy element

        // Shadow
        AddDamageMultiplier(Element.Shadow, Element.Neutral, 0.75f);
        AddDamageMultiplier(Element.Shadow, Element.Water, 1f);
        AddDamageMultiplier(Element.Shadow, Element.Earth, 0.75f);
        AddDamageMultiplier(Element.Shadow, Element.Fire, 1f);
        AddDamageMultiplier(Element.Shadow, Element.Wind, 0.75f);
        AddDamageMultiplier(Element.Shadow, Element.Holy, 1.5f);
        AddDamageMultiplier(Element.Shadow, Element.Shadow, 1.25f);
        AddDamageMultiplier(Element.Shadow, Element.Ghost, 0.75f);
        AddDamageMultiplier(Element.Shadow, Element.Undead, 0.75f);
        // Add damage multipliers for Shadow element

        // Ghost
        AddDamageMultiplier(Element.Ghost, Element.Neutral, 0.5f);
        AddDamageMultiplier(Element.Ghost, Element.Water, 1.25f);
        AddDamageMultiplier(Element.Ghost, Element.Earth, 1f);
        AddDamageMultiplier(Element.Ghost, Element.Fire, 1.5f);
        AddDamageMultiplier(Element.Ghost, Element.Wind, 0.75f);
        AddDamageMultiplier(Element.Ghost, Element.Holy, 1f);
        AddDamageMultiplier(Element.Ghost, Element.Shadow, 1.5f);
        AddDamageMultiplier(Element.Ghost, Element.Ghost, 2f);
        AddDamageMultiplier(Element.Ghost, Element.Undead, 1f);
        // Add damage multipliers for Ghost element

        // Undead
        AddDamageMultiplier(Element.Undead, Element.Neutral, 1f);
        AddDamageMultiplier(Element.Undead, Element.Water, 1.25f);
        AddDamageMultiplier(Element.Undead, Element.Earth, 1.25f);
        AddDamageMultiplier(Element.Undead, Element.Fire, 0.75f);
        AddDamageMultiplier(Element.Undead, Element.Wind, 1.25f);
        AddDamageMultiplier(Element.Undead, Element.Holy, 1.25f);
        AddDamageMultiplier(Element.Undead, Element.Shadow, 0.5f);
        AddDamageMultiplier(Element.Undead, Element.Ghost, 0.75f);
        AddDamageMultiplier(Element.Undead, Element.Undead, -2f);
        // Add damage multipliers for Undead element
    }
}