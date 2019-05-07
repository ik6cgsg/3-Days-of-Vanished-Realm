using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentController : MonoBehaviour
{
    public enum EquipableItem
    {
        NONE,
        TORCH,
        MAGIC_GLASSES,
        // Add more!
        NUM_OF_ITEMS
    }

    [SerializeField]
    private GameObject[] items = new GameObject[(int)EquipableItem.NUM_OF_ITEMS];
    private static GameObject[] staticItems;
    private static IItem currentEquippedItem;
    private static AudioSource soundRef;

    public static UnityEvent equipItemEvent = new UnityEvent();
    public static UnityEvent unequipItemEvent = new UnityEvent();

    public static EquipableItem CurrentItem
    {
        get
        {
            if (currentEquippedItem == null)
            {
                return EquipableItem.NONE;
            }
            return currentEquippedItem.EquipableItem;
        }
    }

    public void Start()
    {
        staticItems = items;
        currentEquippedItem = null;
        soundRef = GetComponent<AudioSource>();
    }

    public static void EquipItem(IItem item)
    {
        soundRef.Play();
        if (currentEquippedItem == item)
        {
            UnequipItem();
            return;
        }

        UnequipItem();
        currentEquippedItem = item;
        currentEquippedItem.Equip();
        HandleItemObject();

        equipItemEvent.Invoke();
    }

    public static void UnequipItem()
    {
        if (currentEquippedItem == null)
        {
            return;
        }

        currentEquippedItem.Unequip();
        HandleItemObject();
        currentEquippedItem = null;

        unequipItemEvent.Invoke();
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
