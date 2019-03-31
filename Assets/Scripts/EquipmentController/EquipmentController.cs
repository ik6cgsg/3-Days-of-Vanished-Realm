using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    public enum EquipableItem
    {
        NONE,
        TORCH,
        MAGIC_GLASSES,
        // Add more!
        NUM_OF_ITEMS,
    }

    [SerializeField]
    private GameObject[] items = new GameObject[(int)EquipableItem.NUM_OF_ITEMS];

    private static GameObject[] staticItems;
    private static IItem currentEquippedItem;

    public void Start()
    {
        staticItems = items;
        currentEquippedItem = null;
    }

    public static void EquipItem(IItem item)
    {
        if (currentEquippedItem == item)
        {
            UnequipItem();
            return;
        }

        UnequipItem();
        currentEquippedItem = item;
        currentEquippedItem.Equip();
        HandleItemObject();
    }

    public static void UnequipItem()
    {
        if (currentEquippedItem == null)
        {
            return;
        }

        currentEquippedItem.Unequip();
        HandleItemObject();
    }

    private static void HandleItemObject()
    {
        GameObject itemObject = staticItems[(int)currentEquippedItem.EquipableItem];
        if (itemObject == null)
        {
            return;
        }

        bool status = itemObject.activeSelf;
        itemObject.SetActive(!status);
    }
}
