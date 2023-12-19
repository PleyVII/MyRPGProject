using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : NewCharacterStats
{
    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
