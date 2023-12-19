using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BowlingBashSkill", menuName = "Skills/BowlingBashSkill")]
public class BowlingBashSkill : DamageSkill
{
    public float areaOfEffectRadius;
    public float stunChance;
    public float stunDuration;

    public override void Cast(GameObject Attacker = null, GameObject target = null, Vector3? WhereToCast = null)
    {
        base.Cast(Attacker, target, WhereToCast);
        Debug.Log("Casting Bowling Bash");
        // Calculate the mana cost based on the skill level and mana multiplier
        float manaCost = manaBaseCost + (manaBaseCost * Mathf.Pow(manaMultiplierPerLevel, levelCurrent));
        Debug.Log("Mana cost: " + manaCost);
        // Perform any visual or audio effects during the charge time
        // After the charge time, execute the skill
        // In this example, we're dealing physical damage with multiple hits to enemies within the area of effect
        int interactableLayerMask = LayerMask.GetMask("Interactable");
        // if (target == null) {Debug.Log("Target Is Null");}
        // Collider[] hitColliders = Physics.OverlapSphere(target.transform.position, areaOfEffectRadius, interactableLayerMask);
        Vector3 WhereToCastNotNull = WhereToCast ?? Vector3.zero;
        Collider[] hitColliders = Physics.OverlapSphere(WhereToCastNotNull, areaOfEffectRadius, interactableLayerMask);
        foreach (Collider collider in hitColliders)
        {
            AllObjectInformation ColiderInformation = collider?.GetComponent<NewCharacterStats>()?.objectInformation;
            ColiderInformation.GetAttacked(physicalDamage * attackerInformation.atk.GetValue() * Mathf.Pow(damageMultiplierPerLevel, levelCurrent), magicDamage, numberOfHits, ColiderInformation.WeaponElement);
            // Apply physical damage to the collider's GameObject
            // ...
            Debug.Log("Dealing " + physicalDamage * attackerInformation.atk.GetValue() * Mathf.Pow(damageMultiplierPerLevel, levelCurrent) * numberOfHits + " physical damage to enemy");

            // Apply stun with a certain chance
            if (Random.value <= stunChance)
            {
                StatusEffect stun = new StatusEffect();
                StatusEffectsManager.instance.AddStatus(Attacker, collider.gameObject, stun);
                // ColiderInformation.currentStatuses.ApplyStatus(attackerInformation, ColiderInformation, Status.Stun, 5);
                // Apply stun to the collider's GameObject for a duration
                // ...
                Debug.Log("Stunned enemy for " + stunDuration + " seconds");
            }
        }
        // Perform any cleanup or additional actions after the skill cast
        // ...
    }
}