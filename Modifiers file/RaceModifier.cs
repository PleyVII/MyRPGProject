using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Race
{
    Angel,
    Brute,
    Human,
    Demon,
    Dragon,
    Fish,
    Formless,
    Insect,
    Plant,
    Undead
}

public class RaceModifier
{
    private Dictionary<Race, List<float>> allCurrentRaceModifiers;
    public RaceModifier()
    {
        // Initialize the dictionary
        allCurrentRaceModifiers = new Dictionary<Race, List<float>>();

        // Initialize the lists for each specificRace
        foreach (Race specificRace in Enum.GetValues(typeof(Race)))
        {
            allCurrentRaceModifiers[specificRace] = new List<float>();
        }
    }

    public void AddRaceModifier(Race specificRace, float raceModifier)
    {
        if (raceModifier != 0)
        {
            if (!allCurrentRaceModifiers.ContainsKey(specificRace))
            {
                allCurrentRaceModifiers[specificRace] = new List<float>();
            }

            allCurrentRaceModifiers[specificRace].Add(raceModifier);
        }
    }

    public void RemoveRaceModifier(Race specificRace, float raceModifier)
    {
        if (allCurrentRaceModifiers.ContainsKey(specificRace))
        {
            allCurrentRaceModifiers[specificRace].Remove(raceModifier);
        }
    }

    public float GetTotalModifiers(Race specificRace)
    {
        float totalModifiers = 0f;

        if (allCurrentRaceModifiers.ContainsKey(specificRace))
        {
            List<float> specificRaceModifiers = allCurrentRaceModifiers[specificRace];

            foreach (float raceModifier in specificRaceModifiers)
            {
                totalModifiers += raceModifier;
            }
        }

        return totalModifiers;
    }
}