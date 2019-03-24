using System.Collections.Generic;
using UnityEngine;

public class GuiItemPanelController : MonoBehaviour
{
    public GameObject itemIconPrefab;
    public GameObject pageTrackerPrefab;

    public float iconScale = 0.3F;
    public float width = 0.5F;
    public int pageSize = 5;

    private int currentPage = 0;
    private int noofPages = 1;
    private List<GameObject> itemIcons = new List<GameObject>();
    private List<GuiItemIconController> itemIconControllers = new List<GuiItemIconController>();
    private GameObject pageTrackerInstance;
    private GuiInventoryController inventoryController;
    private GuiPageTrackerController pageTrackerController;

    void Start()
    {
        inventoryController = transform.parent.GetComponent<GuiInventoryController>();

        // Instantiate item icons as children
        for (int i = 0; i < pageSize; i++)
        {
            itemIcons.Add(Instantiate(itemIconPrefab, transform));
            itemIconControllers.Add(itemIcons[i].GetComponent<GuiItemIconController>());
            inventoryController.UpdateChildTransform(itemIcons[i].transform, 0.5F + ((i + 0.5F) / pageSize - 0.5F) * width);
            //itemIcons[i].transform.localPosition = Vector3.right * ((i + 0.5F) / pageSize - 0.5F) * width;
            itemIcons[i].transform.localScale *= iconScale;
        }

        // Instantiate page tracker
        pageTrackerInstance = Instantiate(pageTrackerPrefab, transform);
        pageTrackerController = pageTrackerInstance.GetComponent<GuiPageTrackerController>();
    }

    private void UpdateItems()
    {
        for (int i = 0; i < pageSize; i++)
            itemIconControllers[i].SetItem(InventoryController.GetItem(i + currentPage * pageSize));
    }

    public void NextPage()
    {
        currentPage = (currentPage + 1) % noofPages;

        // Change itemIcons items
        UpdateItems();

        // Scroll page tracker
        pageTrackerController.SetPage(currentPage);
    }

    public void PrevPage()
    {
        currentPage = currentPage == 0 ? noofPages - 1 : currentPage - 1;

        // Change itemIcons items
        UpdateItems();

        // Scroll page tracker
        pageTrackerController.SetPage(currentPage);
    }

    public void AddPage()
    {
        noofPages++;
        pageTrackerController.AddPage();
    }
}
