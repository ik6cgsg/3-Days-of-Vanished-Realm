using UnityEngine;

public class PickupableObjectController : IInteractiveObject
{
    public string itemName;

    public override void Interact()
    {
        IItem item = (IItem)ScriptableObject.CreateInstance(itemName + "Item");
        InventoryController.AddItem(item);
        Destroy(gameObject);
    }

    public override bool CanInteract()
    {
        return !InventoryController.IsFull();
    }
}
