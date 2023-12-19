using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Provoke", menuName = "Skills/Provoke")]
public class Provoke : BuffSkill
{
    public override void Cast(GameObject Attacker = null, GameObject target = null, Vector3? WhereToCast = null)
    {
        base.Cast(Attacker, target, WhereToCast);
        float manaCost = manaBaseCost + (manaBaseCost * Mathf.Pow(manaMultiplierPerLevel, levelCurrent));
        ProvokeBuff provoke = new ProvokeBuff();
        BuffEffectsManager.instance.AddBuff(Attacker, target, provoke, levelCurrent);
    }
}