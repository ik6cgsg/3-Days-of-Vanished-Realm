using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableItemController : IInteractiveObject
{
    public AudioSource soundRef;
    public override AudioSource Sound
    {
        get
        {
            return soundRef;
        }
    }

    public float burnTime;
    public bool IsOnFire
    {
        get;
        private set;
    }

    private GameObject fire;

    void Start()
    {
        IsOnFire = false;
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.name.Equals("Fire"))
            {
                fire = child.gameObject;
                break;
            }
        }
        fire.SetActive(false);
    }

    public override void Interact()
    {
        if (EquipmentController.CurrentItem == EquipmentController.EquipableItem.TORCH)
        {
            IsOnFire = true;
            fire.SetActive(true);
            VRCursor.SetState(VRCursor.CursorState.NEUTRAL);
            Destroy(GetComponent<InteractiveObjectController>());
            Destroy(gameObject, burnTime);
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
