using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Bash", menuName = "Skills/Bash")]
public class Bash : DamageSkill
{
    public float stunChance = 5;
    public override void Cast(GameObject Attacker = null, GameObject target = null, Vector3? WhereToCast = null)
    {
        base.Cast(Attacker, target, WhereToCast);
        float manaCost = manaBaseCost + (manaBaseCost * Mathf.Pow(manaMultiplierPerLevel, levelCurrent));
        Debug.Log("Casting Bash with Mana cost:" + manaCost);
        targetInfo.GetAttacked(physicalDamage * attackerInformation.atk.GetValue() * Mathf.Pow(damageMultiplierPerLevel, levelCurrent), magicDamage, numberOfHits, attackerInformation.WeaponElement);
        // Apply stun with a certain chance
        if (Random.value <= stunChance * levelCurrent / 100)
        {
            StunEffect stun = new StunEffect();
            StatusEffectsManager.instance.AddStatus(Attacker, target, stun);
            // targetInfo?.currentStatuses?.ApplyStatus(attackerInformation, targetInfo, Status.Stun, 5);
            Debug.Log("Stunned enemy for 5 seconds");
        }
    }
}