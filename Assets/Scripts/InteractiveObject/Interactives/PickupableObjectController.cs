using UnityEngine;

public class PickupableObjectController : IInteractiveObject
{
    public string itemName;
    public AudioSource soundRef;
    Transform[] children;

    private static void SetEnabled(Transform transform, bool enabled)
    {
        if (transform == null)
        {
            return;
        }

        Renderer renderer = transform.GetComponent<Renderer>();
        if (renderer != null)
        {
            transform.GetComponent<Renderer>().enabled = enabled;
        }

        Collider collider = transform.GetComponent<Collider>();
        if (collider != null)
        {
            transform.GetComponent<Collider>().enabled = enabled;
        }
    }

    private void SetChildrenEnabled(bool enabled)
    {
        foreach (Transform child in children)
        {
            SetEnabled(child, enabled);
        }
    }

    private bool isPickedUp = false;

    public override void Save()
    {
        SaveBool("isPickedUp", isPickedUp);
    }

    public override void Load()
    {
        isPickedUp = LoadBool("isPickedUp");

        if (isPickedUp)
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        children = GetComponentsInChildren<Transform>(true);
    }

    public override AudioSource Sound
    {
        get
        {
            return soundRef;
        }
    }

    public override void Interact()
    {
        IItem item = (IItem)ScriptableObject.CreateInstance(itemName + "Item");
        InventoryController.AddItem(item);
        SetChildrenEnabled(false);
        isPickedUp = true;
        Destroy(gameObject, 5);
    }

    public override bool CanInteract()
    {
        return !InventoryController.IsFull();
    }
}
