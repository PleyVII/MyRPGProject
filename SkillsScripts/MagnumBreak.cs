using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagnumBreak", menuName = "Skills/MagnumBreak")]
public class MagnumBreak : DamageSkill
{
    public float areaOfEffectRadius;

    public override void Cast(GameObject Attacker = null, GameObject target = null, Vector3? WhereToCast = null)
    {
        base.Cast(Attacker, target, WhereToCast);
        Debug.Log("Casting Magnum Break");

        // Calculate the mana cost based on the skill level and mana multiplier
        float manaCost = manaBaseCost + (manaBaseCost * Mathf.Pow(manaMultiplierPerLevel, levelCurrent));
        Debug.Log("Casting MagnumBreak with Mana cost:" + manaCost);
        // Perform any visual or audio effects during the charge time
        int interactableLayerMask = LayerMask.GetMask("Interactable");
        Collider[] hitColliders = Physics.OverlapSphere(Attacker.transform.position, areaOfEffectRadius, interactableLayerMask);
        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject != Attacker)
            {
                AllObjectInformation coliderInformation = collider?.GetComponent<NewCharacterStats>()?.objectInformation;
                coliderInformation?.GetAttacked(physicalDamage * attackerInformation.atk.GetValue() * Mathf.Pow(damageMultiplierPerLevel, levelCurrent), magicDamage, numberOfHits, attackerInformation.WeaponElement);
                // Apply physical damage to the collider's GameObject
                // ...
                Debug.Log("Dealing " + physicalDamage * attackerInformation.atk.GetValue() * Mathf.Pow(damageMultiplierPerLevel, levelCurrent) * numberOfHits + " physical damage to enemy");
            }
        }
        // Perform any cleanup or additional actions after the skill cast
        // ...
    }
}
