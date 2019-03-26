using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryController: MonoBehaviour
{
    public static UnityEvent addItemEvent;
    public static UnityEvent increaseSizeEvent;

    private static int maxSize;
    private static List<IItem> items;

    // Adding new item to array
    // Returns false if inventory is full, true otherwise
    public static bool AddItem(IItem item)
    {
        if (IsFull())
        {
            return false;
        }

        items.Add(item);
        addItemEvent.Invoke();
        return true;
    }

    // Removing item from inventory
    public static void RemoveItem(int index)
    {
        items.RemoveAt(index);
    }

    // Getting item from array by its index
    public static IItem GetItem(int index)
    {
        return index >= 0 && index < items.Count
            ? items[index]
            : (IItem)ScriptableObject.CreateInstance("EmptyItem");
    }

    // Getting item from array by its name
    public static IItem GetItem(string name)
    {
        foreach (IItem item in items)
        {
            if (item.Name == name)
            {
                return item;
            }
        }

        return (IItem)ScriptableObject.CreateInstance("EmptyItem");
    }

    // Does we have this item in our inventory
    public static bool HasItem(string name)
    {
        foreach (IItem item in items)
        {
            if (item.Name == name)
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsFull()
    {
        return items.Count == maxSize;
    }

    // Increasing the maximum amount of available items in inventory
    public static void IncreaseSize(int increaseRate)
    {
        maxSize += increaseRate;
        increaseSizeEvent.Invoke();
    }

    public void Start()
    {
        // --- For test ---
        maxSize = 5;
        // ------

        items = new List<IItem>();
        addItemEvent = new UnityEvent();
        increaseSizeEvent = new UnityEvent();
    }
}
