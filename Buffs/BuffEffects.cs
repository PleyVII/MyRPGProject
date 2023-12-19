using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BuffEffect
{
    // Other variables and methods...
    public bool hasBuff = true;
    public float duration = 5f;
    public bool deactivated = false;
    public float currentLevelOfABuff;
    public GameObject buffRecieverVariable;

    public void UpdateStatusDuration(float timePassing)
    {
        duration -= timePassing;

        if (duration <= 0f)
        {
            // Status duration has expired, perform necessary actions
            // such as removing the status or updating the affected object.

            // For example, you can remove the status effect from the object
            // that it is applied to:
            RemoveBuffFromObject();
        }
    }

    private void RemoveBuffFromObject()
    {
        Debug.Log("Some buff was removed");
        // Perform necessary actions to remove the status from the object.
        // This could involve resetting certain variables or disabling specific behaviors.
        hasBuff = false;
    }
    public virtual void ActivateBuff(GameObject buffGiver, GameObject buffReceiver, float CurrentLevelOfABuff)
    {
        currentLevelOfABuff = CurrentLevelOfABuff;
        buffRecieverVariable = buffReceiver;
    }
    public virtual void DeactivateBuff(GameObject WhosBuffsAreGettingDeactivated)
    {
        deactivated = true;
    }
    // Other methods and functionality...
}

public class ProvokeBuff : BuffEffect
{
    public override void ActivateBuff(GameObject buffGiver, GameObject buffReceiver, float CurrentLevelOfABuff)
    {
        base.ActivateBuff(buffGiver, buffReceiver, CurrentLevelOfABuff);
        Debug.Log("Provoke buff activated");
    }
    public override void DeactivateBuff(GameObject WhosBuffsAreGettingDeactivated)
    {
        base.DeactivateBuff(WhosBuffsAreGettingDeactivated);
        Debug.Log("Provoke buff deactivated");
    }
}