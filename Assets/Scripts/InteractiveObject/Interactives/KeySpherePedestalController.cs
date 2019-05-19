using UnityEngine;

public class KeySpherePedestalController : IInteractiveObject
{
    public GameObject keySphere;
    public LogicBoolean logicBoolean;
    public AudioSource soundRef;

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
    }

    public override void Load()
    {
        isEmpty = LoadBool("isEmpty");

        keySphere.SetActive(isEmpty);
    }

    public override bool CanInteract()
    {
        return InventoryController.HasItem("KeySphere");
    }

    public override void Interact()
    {
        logicBoolean.Interact();
        if (!isEmpty)
        {
            InventoryController.AddItem("KeySphere");
        }
        else
        {
            InventoryController.RemoveItem("KeySphere");
        }

        isEmpty = !isEmpty;
        keySphere.SetActive(isEmpty);
    }

    private void Awake()
    {
        keySphere.SetActive(isEmpty);    
    }
}
