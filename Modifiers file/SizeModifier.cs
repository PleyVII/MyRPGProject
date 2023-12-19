using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Size
{
    Large,
    Medium,
    Small
}
public class SizeModifier
{
    private Dictionary<Size, List<float>> sizeBonuses;

    public SizeModifier()
    {
        // Initialize the dictionary
        sizeBonuses = new Dictionary<Size, List<float>>();

        // Initialize the lists for each size
        foreach (Size size in Enum.GetValues(typeof(Size)))
        {
            sizeBonuses[size] = new List<float>();
        }
    }

    public void AddModifier(Size size, float modifier)
    {
        if (modifier != 0)
        {
            if (!sizeBonuses.ContainsKey(size))
            {
                sizeBonuses[size] = new List<float>();
            }

            sizeBonuses[size].Add(modifier);
        }
    }

    public void RemoveModifier(Size size, float modifier)
    {
        if (sizeBonuses.ContainsKey(size))
        {
            sizeBonuses[size].Remove(modifier);
        }
    }

    public float GetTotalModifiers(Size size)
    {
        float totalModifiers = 0f;

        if (sizeBonuses.ContainsKey(size))
        {
            List<float> modifiers = sizeBonuses[size];

            foreach (float modifier in modifiers)
            {
                totalModifiers += modifier;
            }
        }

        return totalModifiers;
    }
}