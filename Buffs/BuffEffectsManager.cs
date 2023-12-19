using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BuffEffectsManager : MonoBehaviour
{
    public Dictionary<GameObject, List<BuffEffect>> objectBuffs = new Dictionary<GameObject, List<BuffEffect>>();
    public static BuffEffectsManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of BuffEffectsManager found");
            return;
        }
        instance = this;
    }

    private void Update()
    {
        List<GameObject> objectsToRemove = new List<GameObject>();

        foreach (KeyValuePair<GameObject, List<BuffEffect>> pair in objectBuffs)
        {
            GameObject gameObjectThatHasBuffs = pair.Key;
            List<BuffEffect> buffs = pair.Value;
            AllObjectInformation objectInfo = gameObjectThatHasBuffs.GetComponent<NewCharacterStats>().objectInformation;

            if (objectInfo != null)
            {
                List<BuffEffect> activeBuffs = new List<BuffEffect>();

                foreach (BuffEffect buffEffect in buffs)
                {
                    if (buffEffect.hasBuff)
                    {
                        buffEffect.UpdateStatusDuration(Time.deltaTime);
                        activeBuffs.Add(buffEffect);
                    }
                    if (!buffEffect.hasBuff && !buffEffect.deactivated)
                    {
                        buffEffect.DeactivateBuff(gameObjectThatHasBuffs);
                    }
                }

                objectInfo.currentBuffs = activeBuffs;

                if (objectInfo.currentBuffs.Count == 0)
                {
                    Debug.Log("An object was removed from the list of buffs");
                    objectsToRemove.Add(gameObjectThatHasBuffs);
                }
            }
        }

        // Remove the objects outside the loop
        foreach (GameObject gameObjectToRemove in objectsToRemove)
        {
            objectBuffs.Remove(gameObjectToRemove);
        }
    }

    public void AddBuff(GameObject buffGiver, GameObject buffReceiver, BuffEffect buffEffect, float currentLevelOfABuff)
    {
        Debug.Log("Add Buff was started");
        if (objectBuffs.ContainsKey(buffReceiver))
        {
            Debug.Log("Add Buff was started");
            List<BuffEffect> buffs = objectBuffs[buffReceiver];
            if (buffs.Count > 0)
            {
                BuffEffect lastBuffEffect = buffs.Last();
                lastBuffEffect.DeactivateBuff(buffReceiver);
            }
            objectBuffs[buffReceiver].Add(buffEffect);
        }
        else
        {
            List<BuffEffect> buffs = new List<BuffEffect> { buffEffect };
            objectBuffs.Add(buffReceiver, buffs);
        }
        buffEffect.ActivateBuff(buffGiver, buffReceiver, currentLevelOfABuff);


        // {
        //     objectBuffs[buffReceiver].Add(buffEffect);
        // }
        // else
        // {
        //     List<BuffEffect> buffs = new List<BuffEffect> { buffEffect };
        //     objectBuffs.Add(buffReceiver, buffs);
        // }
        // buffEffect.ActivateBuff(buffGiver, buffReceiver, currentLevelOfABuff);
    }

    public void RemoveBuff(GameObject gameObject, BuffEffect buffEffect)
    {
        if (objectBuffs.ContainsKey(gameObject))
        {
            objectBuffs[gameObject].Clear();
            objectBuffs.Remove(gameObject);
        }
    }
}