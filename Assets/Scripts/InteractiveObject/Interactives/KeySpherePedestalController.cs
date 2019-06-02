using UnityEngine;

public class KeySpherePedestalController : IInteractiveObject
{
    public GameObject keySphere;
    public AudioSource soundRef;
    public string itemName = "KeySphere";

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

        keySphere.SetActive(!isEmpty);
        GetComponent<InteractiveObjectController>().enabled = isEmpty;
    }

    private void Awake()
    {
        keySphere.GetComponent<PickupableObjectController>().uniqueObjectName = uniqueObjectName + itemName;
        keySphere.SetActive(!isEmpty);
    }

    private void Update()
    {
        isEmpty = !keySphere.activeSelf;
        GetComponent<InteractiveObjectController>().enabled = isEmpty;
    }

    public override bool CanInteract()
    {
        return InventoryController.HasItem(itemName);
    }

    public override void Interact()
    {
        InventoryController.RemoveItem(itemName);
        keySphere.SetActive(true);
        keySphere.GetComponent<InteractiveObjectController>().isWatched = false;
        keySphere.GetComponent<PickupableObjectController>().isPickedUp = false;
        isEmpty = false;
        GetComponent<InteractiveObjectController>().enabled = false;
        VRCursor.SetState(VRCursor.CursorState.NEUTRAL);
    }
}
