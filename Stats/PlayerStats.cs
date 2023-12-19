using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : NewCharacterStats
{
    void Start()
    {
        // EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
            if(newItem != null)
        {
            objectInformation.str.AddModifier(newItem.strengthModifier);
            objectInformation.agi.AddModifier(newItem.agilityModifier);
            objectInformation.vit.AddModifier(newItem.vitalityModifier);
            objectInformation.crit.AddModifier(newItem.criticalModifier);
            objectInformation.spec.AddModifier(newItem.specializeModifier);
            objectInformation._int.AddModifier(newItem.intelligenceModifier);
            objectInformation.dex.AddModifier(newItem.dexterityModifier);
            objectInformation.atk.AddModifier(newItem.attackModifier);
            objectInformation.matk.AddModifier(newItem.magicalattackModifier);
            objectInformation.hit.AddModifier(newItem.hitModifier);
            objectInformation.critChance.AddModifier(newItem.criticalchanceModifier);
            objectInformation.def.AddModifier(newItem.defenceModifier);
            objectInformation.softDef.AddModifier(newItem.softdefenceModifier);
            objectInformation.mDef.AddModifier(newItem.magicaldefenseModifier);
            objectInformation.softMDef.AddModifier(newItem.softmagicaldefenseModifier);
            objectInformation.flee.AddModifier(newItem.fleeModifier);
            objectInformation.aspd.AddModifier(newItem.attackspeedModifier);
            objectInformation.sPoints.AddModifier(newItem.statpointsModifier);
            objectInformation.maxMana.AddModifier(newItem.statpointsModifier);
            objectInformation.manaRegen.AddModifier(newItem.statpointsModifier);
            objectInformation.healthRegen.AddModifier(newItem.statpointsModifier);
            objectInformation.str.AddPercentageModifier(newItem.strengthPercentageModifier);
            objectInformation.agi.AddPercentageModifier(newItem.agilityPercentageModifier);
            objectInformation.vit.AddPercentageModifier(newItem.vitalityPercentageModifier);
            objectInformation.crit.AddPercentageModifier(newItem.criticalPercentageModifier);
            objectInformation.spec.AddPercentageModifier(newItem.specializePercentageModifier);
            objectInformation._int.AddPercentageModifier(newItem.intelligencePercentageModifier);
            objectInformation.dex.AddPercentageModifier(newItem.dexterityPercentageModifier);
            objectInformation.atk.AddPercentageModifier(newItem.attackPercentageModifier);
            objectInformation.matk.AddPercentageModifier(newItem.magicalattackPercentageModifier);
            objectInformation.hit.AddPercentageModifier(newItem.hitPercentageModifier);
            objectInformation.critChance.AddPercentageModifier(newItem.criticalchancePercentageModifier);
            objectInformation.def.AddPercentageModifier(newItem.defencePercentageModifier);
            objectInformation.softDef.AddPercentageModifier(newItem.softdefencePercentageModifier);
            objectInformation.mDef.AddPercentageModifier(newItem.magicaldefensePercentageModifier);
            objectInformation.softMDef.AddPercentageModifier(newItem.softmagicaldefensePercentageModifier);
            objectInformation.flee.AddPercentageModifier(newItem.fleePercentageModifier);
            objectInformation.aspd.AddPercentageModifier(newItem.attackspeedPercentageModifier);
            objectInformation.sPoints.AddPercentageModifier(newItem.statpointsPercentageModifier);
            objectInformation.maxMana.AddPercentageModifier(newItem.statpointsPercentageModifier);
            objectInformation.manaRegen.AddPercentageModifier(newItem.statpointsPercentageModifier);
            objectInformation.healthRegen.AddPercentageModifier(newItem.statpointsPercentageModifier);
        }
        if(oldItem != null)
        {
            objectInformation.str.RemoveModifier(oldItem.strengthModifier);
            objectInformation.agi.RemoveModifier(oldItem.agilityModifier);
            objectInformation.vit.RemoveModifier(oldItem.vitalityModifier);
            objectInformation.crit.RemoveModifier(oldItem.criticalModifier);
            objectInformation.spec.RemoveModifier(oldItem.specializeModifier);
            objectInformation._int.RemoveModifier(oldItem.intelligenceModifier);
            objectInformation.dex.RemoveModifier(oldItem.dexterityModifier);
            objectInformation.atk.RemoveModifier(oldItem.attackModifier);
            objectInformation.matk.RemoveModifier(oldItem.magicalattackModifier);
            objectInformation.hit.RemoveModifier(oldItem.hitModifier);
            objectInformation.critChance.RemoveModifier(oldItem.criticalchanceModifier);
            objectInformation.def.RemoveModifier(oldItem.defenceModifier);
            objectInformation.softDef.RemoveModifier(oldItem.softdefenceModifier);
            objectInformation.mDef.RemoveModifier(oldItem.magicaldefenseModifier);
            objectInformation.softMDef.RemoveModifier(oldItem.softmagicaldefenseModifier);
            objectInformation.flee.RemoveModifier(oldItem.fleeModifier);
            objectInformation.aspd.RemoveModifier(oldItem.attackspeedModifier);
            objectInformation.sPoints.RemoveModifier(oldItem.statpointsModifier);
            objectInformation.maxMana.RemoveModifier(oldItem.statpointsModifier);
            objectInformation.manaRegen.RemoveModifier(oldItem.statpointsModifier);
            objectInformation.healthRegen.RemoveModifier(oldItem.statpointsModifier);
            objectInformation.str.RemovePercentageModifier(oldItem.strengthPercentageModifier);
            objectInformation.agi.RemovePercentageModifier(oldItem.agilityPercentageModifier);
            objectInformation.vit.RemovePercentageModifier(oldItem.vitalityPercentageModifier);
            objectInformation.crit.RemovePercentageModifier(oldItem.criticalPercentageModifier);
            objectInformation.spec.RemovePercentageModifier(oldItem.specializePercentageModifier);
            objectInformation._int.RemovePercentageModifier(oldItem.intelligencePercentageModifier);
            objectInformation.dex.RemovePercentageModifier(oldItem.dexterityPercentageModifier);
            objectInformation.atk.RemovePercentageModifier(oldItem.attackPercentageModifier);
            objectInformation.matk.RemovePercentageModifier(oldItem.magicalattackPercentageModifier);
            objectInformation.hit.RemovePercentageModifier(oldItem.hitPercentageModifier);
            objectInformation.critChance.RemovePercentageModifier(oldItem.criticalchancePercentageModifier);
            objectInformation.def.RemovePercentageModifier(oldItem.defencePercentageModifier);
            objectInformation.softDef.RemovePercentageModifier(oldItem.softdefencePercentageModifier);
            objectInformation.mDef.RemovePercentageModifier(oldItem.magicaldefensePercentageModifier);
            objectInformation.softMDef.RemovePercentageModifier(oldItem.softmagicaldefensePercentageModifier);
            objectInformation.flee.RemovePercentageModifier(oldItem.fleePercentageModifier);
            objectInformation.aspd.RemovePercentageModifier(oldItem.attackspeedPercentageModifier);
            objectInformation.sPoints.RemovePercentageModifier(oldItem.statpointsPercentageModifier);
            objectInformation.maxMana.RemovePercentageModifier(oldItem.statpointsPercentageModifier);
            objectInformation.manaRegen.RemovePercentageModifier(oldItem.statpointsPercentageModifier);
            objectInformation.healthRegen.RemovePercentageModifier(oldItem.statpointsPercentageModifier);
        }
    }
}
