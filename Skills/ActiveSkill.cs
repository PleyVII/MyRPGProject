using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// public void CastSpell(Vector3 castPosition, GameObject target)
// {
//     if (isSingleTarget)
//     {
//         if (target != null)
//         {
//             Enemy enemy = target.GetComponent<Enemy>();
//             if (enemy != null)
//             {
//                 enemy.TakeDamage(damage);
//                 if (enemy.HitCount >= freezeHits)
//                 {
//                     enemy.Freeze();
//                 }
//             }
//         }
//     }
//     else
//     {
//         Collider[] colliders = Physics.OverlapSphere(castPosition, range);
//         foreach (Collider collider in colliders)
//         {
//             Enemy enemy = collider.GetComponent<Enemy>();
//             if (enemy != null)
//             {
//                 enemy.TakeDamage(damage);
//                 if (enemy.HitCount >= freezeHits)
//                 {
//                     enemy.Freeze();
//                 }
//             }
//         }
//     }
[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/BasicSkill")]
public class ActiveSkill : Skill
{
    public bool isTargeted;
    public bool castedAtGround;
    public float range;
    public float manaBaseCost;
    public float manaMultiplierPerLevel = 1.2f;
    public float castTime;
    public float chargeTime;
    public AllObjectInformation attackerInformation;
    public AllObjectInformation targetInfo;
    public virtual void Cast(GameObject Attacker = null, GameObject target = null, Vector3? WhereToCast = null)
    {
        GiveObjectInformationVariables(Attacker, target, WhereToCast);
    }
    public void GiveObjectInformationVariables(GameObject Attacker = null, GameObject target = null, Vector3? WhereToCast = null)
    {
        if (Attacker != null) attackerInformation = Attacker?.GetComponent<NewCharacterStats>()?.objectInformation;                             else attackerInformation = null;
        if (target != null)              targetInfo = target?.GetComponent<NewCharacterStats>()?.objectInformation;                             else target = null;
    }
}
public class DamageSkill : ActiveSkill
{
    public bool isMagicDamage;
    public bool isPhysicalDamage;
    public float physicalDamage;
    public bool IsMelee;
    public bool IsRanged;
    public bool passiveSkill = false;
    public bool isABuff = false;
    public float magicDamage;
    public float damageMultiplierPerLevel = 1.06f;
    public Element skillElementIfNotUsingWeaponElement;
    public int numberOfHits = 1;
}
public class BuffSkill : ActiveSkill
{

}