using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiItemPanelController : MonoBehaviour
{
    public GameObject itemIconPrefab;
    public GameObject pageTrackerPrefab;

    public float iconScale = 0.3F;
    public float width = 2;
    public int pageSize = 5;

    private List<GameObject> itemIcons = new List<GameObject>();
    private GameObject pageTrackerInstance;
    private GuiPageTrackerController pageTrackerController;

    void Start()
    {
        // Instantiate item icons as children
        for (int i = 0; i < pageSize; i++)
        {
            itemIcons.Add(Instantiate(itemIconPrefab, transform));
            itemIcons[i].transform.localPosition = Vector3.right * ((i + 0.5F) / pageSize - 0.5F) * width;
            itemIcons[i].transform.localScale *= iconScale;
        }

        // Instantiate page tracker
    }

    public void NextPage()
    {
        // Change itemIcons items
        // itemIcons[i].SetItem(InventoryController.GetItem(i));
        
        // Scroll page tracker
        //pageTrackerController.NextPage();
    }

    public void PrevPage()
    {
        // Change itemIcons items
        // itemIcons[i].SetItem(InventoryController.GetItem(i));

        // Scroll page tracker
        //pageTrackerController.PrevPage();

    }
}
