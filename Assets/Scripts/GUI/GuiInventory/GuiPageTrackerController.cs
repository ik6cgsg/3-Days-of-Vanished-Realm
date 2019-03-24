using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiPageTrackerController : MonoBehaviour
{
    public GameObject pageTrackerCirclePrefab;
    public float passiveScale = 0.1F;
    public float activeScale = 0.2F;

    private int noofPages = 1;
    private int currentPage = 0;
    private const float SELF_T = 0;
    private const float SELF_T1 = 1;
    private const float SELF_WIDTH = 0.20F;

    private List<GameObject> pageCircles = new List<GameObject>();
    private GuiInventoryController inventoryController;

    private void AddCircle()
    {
        pageCircles.Add(Instantiate(pageTrackerCirclePrefab, transform));
    }

    void Start()
    {
        inventoryController = transform.parent.parent.GetComponent<GuiInventoryController>();

        // Instantiate circles as children
        for (int i = 0; i < noofPages; i++)
        {
            AddCircle();
        }
        UpdateCircleTransforms();
        UpdateCircleScales();
    }

    private void UpdateCircleTransforms()
    {
        for (int i = 0; i < noofPages; i++)
        {
            inventoryController.UpdateChildTransform(pageCircles[i].transform,
                0.5F + ((i + 0.5F) / noofPages - 0.5F) * SELF_WIDTH, SELF_T1);
        }
    }

    private void UpdateCircleScales()
    {
        for (int i = 0; i < noofPages; i++)
        {
            if (i == currentPage)
            {
                pageCircles[i].transform.localScale = new Vector3(activeScale, activeScale, activeScale);
            }
            else
            {
                pageCircles[i].transform.localScale = new Vector3(passiveScale, passiveScale, passiveScale);
            }
        }
    }

    void Update()
    {
    }

    public void SetPage(int newCurrentPage)
    {
        currentPage = newCurrentPage;
        UpdateCircleScales();
    }

    public void AddPage()
    {
        noofPages++;
        Debug.Log("Add page: new noofPage = " + noofPages);
        AddCircle();
        UpdateCircleTransforms();
        UpdateCircleScales();
    }
}
