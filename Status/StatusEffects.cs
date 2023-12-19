using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status
{
    None,
    Stun,
    Freeze,
    Silence,
    Blind,
    Curse,
    LexAeterna,
    Poison,
    Sleep,
    SlowCasting
}

// public class StatusEffect : MonoBehaviour
// {
//     public bool HasStatus { get; set; } = false;
//     public virtual void ApplyStatus(AllObjectInformation attackerInformation, AllObjectInformation targetResistancesInfo, float duration)
//     {

//     }
// }
public class StatusEffect
{
    // Other variables and methods...
    public bool hasStatus = true;
    public float duration = 5f;

    public void UpdateStatusDuration(float timePassing)
    {
        duration -= timePassing;

        if (duration <= 0f)
        {
            // Status duration has expired, perform necessary actions
            // such as removing the status or updating the affected object.

            // For example, you can remove the status effect from the object
            // that it is applied to:
            RemoveStatusFromObject();
        }
    }

    private void RemoveStatusFromObject()
    {
        Debug.Log("Some status was removed");
        // Perform necessary actions to remove the status from the object.
        // This could involve resetting certain variables or disabling specific behaviors.
        hasStatus = false;
    }
    
    public virtual Status GetStatus()
    {
        return Status.None;
    }

    // Other methods and functionality...
}
public class StunEffect : StatusEffect
{
    public override Status GetStatus()
    {
        return Status.Stun;
    }
}
public class FreezeEffect : StatusEffect
{
    public override Status GetStatus()
    {
        return Status.Freeze;
    }
}
public class BlindEffect : StatusEffect
{
    public override Status GetStatus()
    {
        return Status.Blind;
    }
}
public class CurseEffect : StatusEffect
{
    public override Status GetStatus()
    {
        return Status.Curse;
    }
}

// public class StunEffect : StatusEffect
// {
//     private List<float> stunDurations = new List<float>();
//     AllObjectInformation targetResistancesInfoHere;
//     public void ApplyToStatus(AllObjectInformation attackerInformation, AllObjectInformation targetResistancesInfo, float duration)
//     {
//         Debug.Log("Applied Stun");
//         targetResistancesInfoHere = targetResistancesInfo;
//         stunDurations.Add(duration);
//         HasStatus = true;
//     }

//     public void Update()
//     {
//         if (stunDurations.Count > 0)
//         {
//             float deltaTime = Time.deltaTime;

//             for (int i = stunDurations.Count - 1; i >= 0; i--)
//             {
//                 stunDurations[i] -= deltaTime;

//                 if (stunDurations[i] <= 0f)
//                 {
//                     stunDurations.RemoveAt(i);
//                 }
//             }

//             if (stunDurations.Count == 0)
//             {
//                 HasStatus = false;
//                 targetResistancesInfoHere.currentStatuses.RemoveStatus(Status.Stun);
//             }
//         }
//     }
// }



// public class FreezeEffect : StatusEffect
// {
//     public override void ApplyStatus(AllObjectInformation attackerInformation, AllObjectInformation targetResistancesInfo, float duration)
//     {
//         // Apply freeze effect logic here
//     }
// }