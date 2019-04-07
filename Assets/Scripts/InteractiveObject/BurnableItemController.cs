using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableItemController : IInteractiveObject
{
    public GameObject burnableObject;
    public float burnTime;
    public bool IsOnFire
    {
        get;
        private set;
    }

    private GameObject fire;
    private float fireStartTime;

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

    void Update()
    {
        if (IsOnFire && Time.time - fireStartTime >= burnTime)
        {
            Destroy(burnableObject);
            IsOnFire = false;
        }
    }

    public override void Interact()
    {
        if (EquipmentController.CurrentItem == EquipmentController.EquipableItem.TORCH)
        {
            IsOnFire = true;
            fire.SetActive(true);
            fireStartTime = Time.time;
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
