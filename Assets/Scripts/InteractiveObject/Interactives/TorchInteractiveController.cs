using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchInteractiveController : IInteractiveObject
{
    private GameObject fire;

    // Start is called before the first frame update
    void Start()
    {
        fire = GameObject.Find("Cylinder/Fire");
        fire.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void Interact()
    {
        if (EquipmentController.CurrentItem == EquipmentController.EquipableItem.TORCH)
        {
            fire.SetActive(true);
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
