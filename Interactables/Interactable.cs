using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 1f;
    public Item item;
    public Interactable focusedObjectInteractable;
    public AllObjectInformation objectInformation;
    public bool isFocused = false;
    public bool isItem = true;
    public Transform focus;
    public GameObject focusedObject;
    public bool isAttackOrAbility = true;
    private void Awake()
    {
        if (item != null) isItem = true;
    }
    public virtual void InteractPickUpItems()
    {
        float distance = Vector3.Distance(this.transform.position, focusedObject.transform.position);
        if (distance <= item.pickUpRadius && focusedObjectInteractable.isItem) Pickup();
    }
    public virtual void InteractWithAutoAttack()
    {

    }
    public virtual void InteractWithSkill()
    {

    }
    public void Pickup()
    {
        Debug.Log("Picking up" + focusedObjectInteractable.item.name);
        bool wasPickedUp = Inventory.instance.AddToList(focusedObjectInteractable.item);
        if (wasPickedUp)
            Destroy(focusedObject);
    }

    public virtual void Update()
    {

    }
    public void OnFocused(GameObject playerGO)
    {
        isFocused = true;
        // delegate castingspell = true;
        focus = playerGO.transform;
        objectInformation.focusedObject = playerGO;
        focusedObject = playerGO;
        focusedObjectInteractable = focusedObject.gameObject.GetComponent<Interactable>();
    }
    public void OnDefocused()
    {
        isFocused = false;
        focusedObject = null;
        focus = null;
    }
}