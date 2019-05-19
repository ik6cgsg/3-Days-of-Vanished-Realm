using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryController: MonoBehaviour
{
    public static UnityEvent addItemEvent = new UnityEvent();
    public static UnityEvent removeItemEvent = new UnityEvent();
    public static UnityEvent increaseSizeEvent = new UnityEvent();

    private static int maxSize;
    private static List<IItem> items = new List<IItem>();

    private static IItem emptyItem;

    private static IItem getEmptyItem()
    {
        if (emptyItem == null)
        {
            emptyItem = (IItem)ScriptableObject.CreateInstance("EmptyItem");
        }

        return emptyItem;
    }

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

    public static bool AddItem(string itemName)
    {
        if (IsFull())
        {
            return false;
        }

        items.Add((IItem)ScriptableObject.CreateInstance(itemName + "Item"));
        addItemEvent.Invoke();
        return true;
    }

    // Removing item from inventory
    public static void RemoveItem(string name)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Name.Equals(name))
            {
                items.RemoveAt(i);
                removeItemEvent.Invoke();
                break;
            }
        }
    }

    // Getting item from array by its index
    public static IItem GetItem(int index)
    {
        return index >= 0 && index < items.Count
            ? items[index]
            : getEmptyItem();
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

        return getEmptyItem();
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

    private void Start()
    {
        // --- For test ---
        maxSize = 10;
        // ------

        for (int i = 0; i < maxSize; i++)
        {
            string itemName = PlayerPrefs.GetString("InventoryItem" + i);
        
            if (!itemName.Equals(""))
            {
                AddItem(itemName);
            }
        }
    }

    private void OnApplicationPause()
    {
        for (int i = 0; i < maxSize; i++)
        {
            PlayerPrefs.SetString("InventoryItem" + i, i < items.Count ? items[i].Name : "");
        }

        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < maxSize; i++)
        {
            PlayerPrefs.SetString("InventoryItem" + i, i < items.Count ? items[i].Name : "");
        }

        PlayerPrefs.Save();
    }
}
