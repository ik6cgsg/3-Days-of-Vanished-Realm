using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiItemPanelController : MonoBehaviour
{
    public GameObject itemIconPrefab;
    public GameObject pageTrackerPrefab;

    private List<GameObject> itemIcons;
    private GameObject pageTrackerInstance;
    private GuiPageTrackerController pageTrackerController;

    void Start()
    {
        ; // Instantiate item icons as children
    }

    public void NextPage()
    {
        // Change itemIcons items
        // itemIcons[i].SetItem(InventoryController.GetItem(i));
        
        // Scroll page tracker
        pageTrackerController.NextPage();
    }

    public void PrevPage()
    {
        // Change itemIcons items
        // itemIcons[i].SetItem(InventoryController.GetItem(i));

        // Scroll page tracker
        pageTrackerController.PrevPage();

    }
}
