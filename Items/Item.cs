using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public float pickUpRadius = 2f;

    public virtual void Use ()
    {
        Debug.Log("Using " + name);
    }
    public void RemoveFromInventory()
    {
        Inventory.instance.RemoveFromList(this);
    }
}