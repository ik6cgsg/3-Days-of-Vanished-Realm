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

    public bool isLitAtStart = false;

    public override void Save()
    {
        SaveBool("isLit", IsLit);
    }

    public override void Load()
    {
        IsLit = LoadBool("isLit");

        if (IsLit)
        {
            fire.SetActive(true);
            Destroy(GetComponent<InteractiveObjectController>());
        }
    }

    void Awake()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform   child in children)
        {
            if (child.name.Equals("Fire"))
            {
                fire = child.gameObject;
                break;
            }
        }
        fire.SetActive(isLitAtStart);
        IsLit = isLitAtStart;
        if (isLitAtStart)
        {
            soundRef.Play();
            Destroy(GetComponent<InteractiveObjectController>());
        }
    }

    public override void Interact()
    {
        fire.SetActive(true);
        IsLit = true;
        Destroy(GetComponent<InteractiveObjectController>());
        VRCursor.SetState(VRCursor.CursorState.NEUTRAL);
    }

    public override bool CanInteract()
    {
        if (!isLitAtStart && EquipmentController.CurrentItem == EquipmentController.EquipableItem.TORCH)
        {
            return true;
        }
        return false;
    }
}
