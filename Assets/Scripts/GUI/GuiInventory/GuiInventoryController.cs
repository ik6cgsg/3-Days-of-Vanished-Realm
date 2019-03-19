using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiInventoryController : MonoBehaviour
{
    public Transform collapseButton;
    public Transform prevPageButton;
    public Transform itemPanel;
    public Transform nextPageButton;

    private float COLLAPSE_T;
    private float PREV_PAGE_T;
    private float ITEM_PANEL_T;
    private float NEXT_PAGE_T;

    public static void UpdateChildTransform(Transform transform, float t)
    {
        // transform.position = ...
        // transform.rotation = ...
    }

    void Start()
    {
    }

    void Update()
    {
        // Check if looking up
        // Show the whole shabang
        // Or hide the whole kerfuffle
    }

    void Show()
    {
        UpdateChildrenTransform();
        UpdateSelfTransform();

        // Do the thing
    }

    void Hide()
    {
        // Do the thing, but in reverse
    }

    private void UpdateSelfTransform()
    {
        // Rotate around Ox
        // Rotate around Oy
    }

    private void UpdateChildrenTransform()
    {
        UpdateChildTransform(collapseButton, COLLAPSE_T);
        UpdateChildTransform(prevPageButton, PREV_PAGE_T);
        UpdateChildTransform(itemPanel, ITEM_PANEL_T);
        UpdateChildTransform(nextPageButton, NEXT_PAGE_T);
    }

    public void Collapse()
    {
        // Disable rendering for starters?
    }
}
