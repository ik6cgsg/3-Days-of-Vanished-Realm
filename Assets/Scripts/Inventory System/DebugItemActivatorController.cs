using System;
using UnityEngine;

public class DebugItemActivatorController : MonoBehaviour
{
    public string[] debugItemNames = new string[10];

    private IItem[] debugItems;

    private void Start()
    {
        debugItems = new IItem[debugItemNames.Length];

        for (int i = 0; i < Math.Min(debugItems.Length, 10); i++)
        {
            if (debugItemNames[i] != "")
            {
                debugItems[i] = (IItem)ScriptableObject.CreateInstance(debugItemNames[i] + "Item");
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < Math.Min(debugItems.Length, 10); i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                Debug.Log("Pressed " + i);

                if (debugItems[i] == null)
                {
                    Debug.Log("Item is null");
                }
                else if (debugItems[i].IsEquipable())
                {
                    Debug.Log("Using " + debugItems[i].Name);
                    EquipmentController.EquipItem(debugItems[i]);
                }
                else
                {
                    Debug.Log(debugItems[i].Name + " is not usable");
                }
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("Purged save data");
                PlayerPrefs.DeleteAll();
                SaveSystemController saveSystem = FindObjectOfType<SaveSystemController>();
                saveSystem.enabled = false;
            }
        }
    }
}
