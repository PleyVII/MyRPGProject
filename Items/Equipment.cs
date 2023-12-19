using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    #region Modifiers
    public int strengthModifier;
    public int agilityModifier;
    public int vitalityModifier;
    public int specializeModifier;
    public int criticalModifier;
    public int intelligenceModifier;
    public int dexterityModifier;
    public int attackModifier;
    public int magicalattackModifier;
    public int hitModifier;
    public int criticalchanceModifier;
    public int defenceModifier;
    public int softdefenceModifier;
    public int magicaldefenseModifier;
    public int softmagicaldefenseModifier;
    public int fleeModifier;
    public int attackspeedModifier;
    public int statpointsModifier;
    #endregion
    #region PercentageModifiers
    public int strengthPercentageModifier;
    public int agilityPercentageModifier;
    public int vitalityPercentageModifier;
    public int specializePercentageModifier;
    public int criticalPercentageModifier;
    public int intelligencePercentageModifier;
    public int dexterityPercentageModifier;
    public int attackPercentageModifier;
    public int magicalattackPercentageModifier;
    public int hitPercentageModifier;
    public int criticalchancePercentageModifier;
    public int defencePercentageModifier;
    public int softdefencePercentageModifier;
    public int magicaldefensePercentageModifier;
    public int softmagicaldefensePercentageModifier;
    public int fleePercentageModifier;
    public int attackspeedPercentageModifier;
    public int statpointsPercentageModifier;
    #endregion
    
    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
        // equip and remove from inventory
    }
}
public enum EquipmentSlot
{
    Helmet, BodyArmor, Pants, Boots, Shield, Weapon
}