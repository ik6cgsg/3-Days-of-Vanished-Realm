using UnityEngine;

public class KeySpherePedestalController : IInteractiveObject
{
    public GameObject keySphere;
    public AudioSource soundRef;
    public string itemName = "KeySphere";

    private PickupableObjectController pickupable;

    public override AudioSource Sound
    {
        get
        {
            return soundRef;
        }
    }

    public bool isEmpty = true;

    public override void Save()
    {
        SaveBool("isEmpty", isEmpty);
        Debug.Log(uniqueObjectName + isEmpty.ToString());
    }

    public override void Load()
    {
        isEmpty = LoadBool("isEmpty");
        Debug.Log(uniqueObjectName + isEmpty.ToString());

        pickupable.SetEnabled(!isEmpty);
        GetComponent<InteractiveObjectController>().enabled = isEmpty;
    }

    private void Awake()
    {
        keySphere.GetComponentInChildren<PickupableObjectController>().uniqueObjectName = uniqueObjectName + itemName;
        pickupable = keySphere.GetComponent<PickupableObjectController>();
        pickupable.SetEnabled(!isEmpty);
    }

    private void Update()
    {
        isEmpty = pickupable.isPickedUp;
        GetComponent<InteractiveObjectController>().enabled = isEmpty;
    }

    public override bool CanInteract()
    {
        return InventoryController.HasItem(itemName);
    }

    public override void Interact()
    {
        InventoryController.RemoveItem(itemName);
        pickupable.SetEnabled(true);
        keySphere.GetComponent<InteractiveObjectController>().isWatched = false;
        pickupable.isPickedUp = false;
        isEmpty = false;
        GetComponent<InteractiveObjectController>().enabled = false;
        VRCursor.SetState(VRCursor.CursorState.NEUTRAL);
    }
}
