using System;
using UnityEngine;

public class DebugItemActivatorController : MonoBehaviour
{
    public IItem[] debugItems = new IItem[10];

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
                else if (debugItems[i].IsUsable())
                {
                    Debug.Log("Using " + debugItems[i].Name);
                    debugItems[i].Use();
                }
                else
                {
                    Debug.Log(debugItems[i].Name + " is not usable");
                }
            }
        }
    }
}
