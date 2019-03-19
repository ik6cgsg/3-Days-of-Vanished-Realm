using UnityEngine;

public class InventoryController: MonoBehaviour
{
    private static int maxSize;
    private static int maxAvailableSize;
    private static IItem[] items;

    // Adding new item to array
    // Returns false if inventory is full, true otherwise
    public static bool AddItem(IItem item)
    {
        return false;
    }

    // Dropping item from inventory to the environment
    public static void DropItem(int index)
    {

    }

    // Getting item from array by its index
    public static IItem GetItem(int index)
    {
        return new EmptyItem();
    }

    // Getting item from array by its name
    public static IItem GetItem(string name)
    {
        return new EmptyItem();
    }

    // Does we have this item in our inventory
    public static bool HasItem(IItem item)
    {
        return false;
    }

    public static bool IsFull()
    {
        return false;
    }

    // Increasing the maximum amount of items in inventory
    public static void IncreaseSize(int increaseRate)
    {

    }

    // Increasing the maximum amount of available items in inventory
    public static void IncreaseAvailableSize(int increaseRate)
    {

    }

    public void Start()
    {

    }
}
