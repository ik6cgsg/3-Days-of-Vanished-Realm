using System.Collections.Generic;
using UnityEngine;

public class InventoryController: MonoBehaviour
{
    private static int maxSize;
    private static int maxAvailableSize;
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
        return index >= 0 && index < items.Count ? items[index] : new EmptyItem();
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

        return new EmptyItem();
    }

    // Does we have this item in our inventory
    public static bool HasItem(IItem item)
    {
        return items.Contains(item);
    }

    public static bool IsFull()
    {
        return items.Count == maxAvailableSize;
    }

    // Increasing the maximum amount of items in inventory
    public static void IncreaseSize(int increaseRate)
    {
        maxSize += increaseRate;
    }

    // Increasing the maximum amount of available items in inventory
    public static void IncreaseAvailableSize(int increaseRate)
    {
        maxAvailableSize += increaseRate;
    }

    public void Start()
    {
        // --- For test ---
        maxSize = 4;
        maxAvailableSize = 1;
        // ------

        items = new List<IItem>();
    }
}
