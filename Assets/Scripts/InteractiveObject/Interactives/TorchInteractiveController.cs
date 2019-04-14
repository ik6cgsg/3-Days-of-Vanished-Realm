using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchInteractiveController : IInteractiveObject
{
    public AudioSource soundRef;
    public override AudioSource Sound
    {
        get
        {
            return soundRef;
        }
    }

    private GameObject fire;

    public bool IsLit
    {
        get;
        private set;
    }

    // Start is called before the first frame update
    void Start()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        Debug.Log(children);
        foreach (Transform child in children)
        {
            if (child.name.Equals("Fire"))
            {
                fire = child.gameObject;
                break;
            }
        }
        fire.SetActive(false);
        IsLit = false;
    }

    public override void Interact()
    {
        if (EquipmentController.CurrentItem == EquipmentController.EquipableItem.TORCH)
        {
            fire.SetActive(true);
            IsLit = true;
            Destroy(GetComponent<InteractiveObjectController>());
            VRCursor.SetState(VRCursor.CursorState.NEUTRAL);
        }
    }

    public override bool CanInteract()
    {
        if (EquipmentController.CurrentItem == EquipmentController.EquipableItem.TORCH)
        {
            return true;
        }
        return false;
    }
}
