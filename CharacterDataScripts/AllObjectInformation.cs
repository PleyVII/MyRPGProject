using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllObjectInformation", menuName = "ObjectValues")]
public class AllObjectInformation : ScriptableObject
{
    #region Stats
    private Stat str, agi, vit, spec, crit, _int, dex, def, mDef, atk, aspd, flee, matk, hit, critChance, softMDef, softDef, sPoints, maxHealth, healthRegen, maxMana, manaRegen, weaponAtk, weaponRange;
    public float realDef, realMDef;
    private void InitializeStats()
    {
        str = new Stat(StatIdentifierEnum.Str);
        agi = new Stat(StatIdentifierEnum.Agi);
        vit = new Stat(StatIdentifierEnum.Vit);
        spec = new Stat(StatIdentifierEnum.Spec);
        crit = new Stat(StatIdentifierEnum.Crit);
        _int = new Stat(StatIdentifierEnum.Int);
        dex = new Stat(StatIdentifierEnum.Dex);
        def = new Stat(StatIdentifierEnum.Def);
        mDef = new Stat(StatIdentifierEnum.MDef);
        atk = new Stat(StatIdentifierEnum.Atk);
        aspd = new Stat(StatIdentifierEnum.Aspd);
        flee = new Stat(StatIdentifierEnum.Flee);
        matk = new Stat(StatIdentifierEnum.Matk);
        hit = new Stat(StatIdentifierEnum.Hit);
        critChance = new Stat(StatIdentifierEnum.CritChance);
        softMDef = new Stat(StatIdentifierEnum.SoftMDef);
        softDef = new Stat(StatIdentifierEnum.SoftDef);
        sPoints = new Stat(StatIdentifierEnum.SPoints);
        maxHealth = new Stat(StatIdentifierEnum.MaxHealth);
        healthRegen = new Stat(StatIdentifierEnum.HealthRegen);
        maxMana = new Stat(StatIdentifierEnum.MaxMana);
        manaRegen = new Stat(StatIdentifierEnum.ManaRegen);
        weaponAtk = new Stat(StatIdentifierEnum.WeaponAtk);
        weaponRange = new Stat(StatIdentifierEnum.WeaponRange);
    }
    private void InitializeStatsDictionary()
    {
        dictionaryOfAllStatVariables = new Dictionary<StatIdentifierEnum, Stat>();
        foreach (FieldInfo field in GetType().GetFields())
        {
            if (field.FieldType == typeof(Stat))
            {
                Stat specificStatVariable = (Stat)field.GetValue(this);
                if (specificStatVariable != null)
                {
                    dictionaryOfAllStatVariables[specificStatVariable.thisStatIdentifier_Enum] = specificStatVariable;
                }
            }
        }
    }
    #endregion
    #region Properties
    public float Str
    {
        get { return str.GetValue(); }
        set { str.baseValue = value; }
    }
    public float Agi
    {
        get { return agi.GetValue(); }
        set { agi.baseValue = value; }
    }
    public float Vit
    {
        get { return vit.GetValue(); }
        set { vit.baseValue = value; }
    }
    public float Spec
    {
        get { return spec.GetValue(); }
        set { spec.baseValue = value; }
    }

    public float Crit
    {
        get { return crit.GetValue(); }
        set { crit.baseValue = value; }
    }

    public float Int
    {
        get { return _int.GetValue(); } // Assuming '_int' is the name due to 'int' being a reserved keyword in C#
        set { _int.baseValue = value; }
    }

    public float Dex
    {
        get { return dex.GetValue(); }
        set { dex.baseValue = value; }
    }
    public float WeaponAtk
    {
        get { return weaponAtk.GetValue(); }
        set { weaponAtk.baseValue = value; }
    }
    public float Def
    {
        get { return def.GetValue(); }
        set { def.baseValue = value; }
    }
    public float MDef
    {
        get { return mDef.GetValue(); }
        set { mDef.baseValue = value; }
    }
    public float WeaponRange
    {
        get { return weaponRange.GetValue(); }
        set { weaponRange.baseValue = value; }
    }
    public float Atk
    {
        get
        {
            atk.baseValue = 50 + (2 * Str + (Mathf.Floor(Str / 10) * 30));
            return atk.GetValue();
        }
    }
    public float Aspd
    {
        get
        {
            aspd.baseValue = 50 + (Agi * 2);
            return aspd.GetValue();
        }
    }
    public float Flee
    {
        get
        {
            flee.baseValue = characterLvl + (4 * Agi);
            return flee.GetValue();
        }
    }
    public float Matk
    {
        get
        {
            matk.baseValue = 50 + (2 * Int + (Mathf.Floor(Int / 10) * 30));
            return matk.GetValue();
        }
    }
    public float Hit
    {
        get
        {
            hit.baseValue = characterLvl + (4 * Dex);
            return hit.GetValue();
        }
    }
    public float CritChance
    {
        get
        {
            critChance.baseValue = Crit;
            return critChance.GetValue();
        }
    }
    public float SoftMDef
    {
        get
        {
            softMDef.baseValue = Int * 3;
            return softMDef.GetValue();
        }
    }
    public float SoftDef
    {
        get
        {
            softDef.baseValue = Vit * 3;
            return softDef.GetValue();
        }
    }
    public float MaxHealth
    {
        get { return maxHealth.GetValue(); }
        set { maxHealth.baseValue = value; }
    }
    public float MaxMana
    {
        get { return maxMana.GetValue(); }
        set { maxMana.baseValue = value; }
    }

    public float HealthRegen
    {
        get
        {
            healthRegen.baseValue = (MaxHealth / 50) + (2 * (MaxHealth / 100 * Vit));
            return healthRegen.GetValue();
        }
    }

    public float ManaRegen
    {
        get
        {
            manaRegen.baseValue = (MaxMana / 50) + (2 * (MaxMana / 100 * Int));
            return manaRegen.GetValue();
        }
    }
    #endregion
    #region Stat Calculations
    private void CalculateMaxHealth()
    {
        float healthMultiplier = GetHealthMultiplier();

        maxHealth.baseValue = Mathf.RoundToInt(2000f + 20f * characterLvl * healthMultiplier * (vit.GetValue() / 70));
    }
    private void CalculateMaxMana()
    {
        float manaMultiplier = GetManaMultiplier();

        maxMana.baseValue = Mathf.RoundToInt(200f + 2f * characterLvl * manaMultiplier * (_int.GetValue() / 70));
    }
    private void CalculateSPoints()
    {
        int statVariable = 0;
        for (int i = 0; i < characterLvl; i++)
        {
            statVariable += (5 + (int)Mathf.Floor(i / 10)) * 4;
        }
        sPoints.baseValue = statVariable;
    }
    private void CalculateRealDef()
    {
        float defValue = def.GetValue();

        if (defValue <= 25)
        {
            realDef = defValue * 2;
        }
        else if (defValue > 25 && defValue <= 50)
        {
            realDef = defValue + 25;
        }
        else if (defValue > 50)
        {
            realDef = defValue + 25 - ((defValue - 50) / 2);
        }
    }
    private void CalculateRealMDef()
    {
        float mDefValue = mDef.GetValue();

        if (mDefValue <= 25)
        {
            realMDef = mDefValue * 2;
        }
        else if (mDefValue > 25 && mDefValue <= 50)
        {
            realMDef = mDefValue + 25;
        }
        else if (mDefValue > 50)
        {
            realMDef = mDefValue + 25 - ((mDefValue - 50) / 2);
        }
    }
    #endregion
    #region Character Classes and its Health and Mana Multipliers
    public enum CharacterClass
    {
        None, Novice, Swordsman, Mage, Cartboy, Acolyte, Thief, Archer, Kingsguard, RoyalTemplar, Guardian, Archimage, Scholar, Enchanter, Creator, Blacksmith, Zoologist,
        Aun, Bishop, Occultist, Assassin, Scoundrel, Reaper, Sniper, Bard, Hunter
    }
    public CharacterClass characterClass;
    private float GetHealthMultiplier()
    {
        switch (characterClass)
        {
            case CharacterClass.Novice:
            case CharacterClass.Mage:
            case CharacterClass.Acolyte:
            case CharacterClass.Archimage:
            case CharacterClass.Scholar:
            case CharacterClass.Bishop:
            case CharacterClass.Occultist:
                return 1.4f;
            case CharacterClass.Cartboy:
            case CharacterClass.Archer:
            case CharacterClass.Bard:
            case CharacterClass.Sniper:
            case CharacterClass.Hunter:
            case CharacterClass.Creator:
            case CharacterClass.Zoologist:
            case CharacterClass.Enchanter:
                return 1.7f;
            case CharacterClass.Aun:
            case CharacterClass.Assassin:
            case CharacterClass.Scoundrel:
            case CharacterClass.Reaper:
            case CharacterClass.Blacksmith:
            case CharacterClass.Thief:
                return 1.9f;
            case CharacterClass.Swordsman:
            case CharacterClass.Guardian:
            case CharacterClass.Kingsguard:
            case CharacterClass.RoyalTemplar:
                return 2.2f;
            default:
                return 1f;
        }
    }
    private float GetManaMultiplier()
    {
        switch (characterClass)
        {
            case CharacterClass.Novice:
                return 1f;
            case CharacterClass.Mage:
            case CharacterClass.Archimage:
            case CharacterClass.Scholar:
                return 3f;
            case CharacterClass.Acolyte:
            case CharacterClass.Bishop:
            case CharacterClass.Occultist:
            case CharacterClass.Bard:
            case CharacterClass.Aun:
                return 2f;
            case CharacterClass.Archer:
            case CharacterClass.Sniper:
            case CharacterClass.Hunter:
            case CharacterClass.Creator:
            case CharacterClass.Zoologist:
            case CharacterClass.Enchanter:
                return 1.7f;
            case CharacterClass.Cartboy:
            case CharacterClass.Assassin:
            case CharacterClass.Scoundrel:
            case CharacterClass.Reaper:
            case CharacterClass.Blacksmith:
            case CharacterClass.Thief:
                return 1.4f;
            case CharacterClass.Swordsman:
            case CharacterClass.Guardian:
            case CharacterClass.Kingsguard:
            case CharacterClass.RoyalTemplar:
                return 1.5f;
            default:
                return 1f;
        }
    }
    #endregion

    public float characterLvl = 1, jobLvl = 1, statvarible, baseExpToLvlUp, baseExpCurrent, jobExpToLvlUp, jobExpCurrent, allPointsUsed = 0;
    private float currentHealth, currentMana;
    public float CurrentHealth { get { return currentHealth; } set { currentHealth = Mathf.Clamp(value, 0, MaxHealth); } }
    public float CurrentMana { get { return currentMana; } set { currentMana = Mathf.Clamp(value, 0, MaxMana); } }
    public Element BodyElement = Element.Neutral, WeaponElement = Element.Neutral;
    public Race Race = Race.Human;
    public List<Status> currentStatuses;
    public List<BuffEffect> currentBuffs;
    private Dictionary<StatIdentifierEnum, Stat> dictionaryOfAllStatVariables;
    public float attackCooldown = 0, abilityCooldown = 0;
    public GameObject focusedObject;
    public static event System.Action OnGettingAttacked;
    public AllObjectInformation()
    {
        InitializeStats();
        InitializeStatsDictionary();
    }
    public void GetAttacked(float physical, float magical, float numberOfHits, Element element)
    {
        float physicalDamage = Mathf.Clamp((physical * (100 - realDef) / 100 * ElementModifier.GetDamageMultiplier(element, BodyElement)) - numberOfHits * SoftDef, 1, float.MaxValue) * numberOfHits;
        float magicDamage = Mathf.Clamp((magical * (100 - realMDef) / 100 * ElementModifier.GetDamageMultiplier(element, BodyElement)) - numberOfHits * SoftMDef, 1, float.MaxValue) * numberOfHits;
        CurrentHealth -= physicalDamage + magicDamage;
        Debug.Log("Hit for " + (physicalDamage + magicDamage) + " damage" + ", " + CurrentHealth + "hp left");
        OnGettingAttacked?.Invoke();
    }
    public void ApplyStatus(AllObjectInformation ThisTargetInformation, Status Stun, float duration)
    {
        //Apply status for a duration
    }
    public void UsePoints(float usedPoints)
    {
        allPointsUsed += usedPoints;
    }
}