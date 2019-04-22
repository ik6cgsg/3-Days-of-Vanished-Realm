﻿using UnityEngine;

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

    private void Start()
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
        Destroy(gameObject, 5);
    }

    public override bool CanInteract()
    {
        return !InventoryController.IsFull();
    }
}
