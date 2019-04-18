using UnityEngine;

public class KeySpherePedestalController : IInteractiveObject
{
    public GameObject keySphere;
    public LogicBoolean logicBoolean;

    public override bool CanInteract()
    {
        return InventoryController.HasItem("KeySphere");
    }

    public override void Interact()
    {
        logicBoolean.Interact();
        keySphere.SetActive(true);
        GetComponent<InteractiveObjectController>().enabled = false;
        VRCursor.SetState(VRCursor.CursorState.NEUTRAL);
        InventoryController.RemoveItem("KeySphere");
    }

    private void Start()
    {
        keySphere.SetActive(false);    
    }
}
