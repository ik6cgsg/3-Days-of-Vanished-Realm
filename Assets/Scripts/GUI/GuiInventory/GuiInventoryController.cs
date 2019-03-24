using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiInventoryController : MonoBehaviour
{
    public GameObject collapseButton;
    public GameObject prevPageButton;
    public GameObject itemPanel;
    public GameObject nextPageButton;

    public float distance = 3;
    public float lookUpAngle = 20;
    public float heightAngle = 45;
    public float widthAngle = 90;
    public float thickAngle = 20;

    public Transform mainCameraTransform;

    private float COLLAPSE_T = 0.075F;
    private float PREV_PAGE_T = 0.15F;
    private float NEXT_PAGE_T = 0.85F;

    private bool isShown = false;
    private bool isCollapsed = false;

    public void UpdateChildTransform(Transform transform, float t, float t1 = 0.5F)
    {
        Vector3 pos = new Vector3(0, 0, distance);
        Quaternion rotate = Quaternion.Euler(thickAngle * (t1 - 0.5F),
                                             widthAngle * (t - 0.5F),
                                             0);

        pos = rotate * pos;

        transform.localPosition = pos;
        transform.localRotation = rotate;
    }

    void Start()
    {
        Hide();
    }

    private bool IsLookUp( float xAngle )
    {
        return xAngle > 90 && xAngle <= 360 - lookUpAngle;
    }

    void Update()
    {
        // Check if shown
        if (isShown)
        {
            if (!IsLookUp(mainCameraTransform.rotation.eulerAngles.x))
            {
                // Hide the whole kerfuffle
                Hide();
            }
        }
        else
        {
            // Check if looking up
            if (IsLookUp(mainCameraTransform.rotation.eulerAngles.x))
            {
                // Show the whole shabang
                Show();
            }
        }
    }

    void Show()
    {
        Debug.Log("Show GUI");
        isShown = true;
        UpdateChildrenTransform();
        UpdateSelfTransform();

        // Do the thing
        EnableChildren(true);
    }

    void Hide()
    {
        Debug.Log("Hide GUI");
        isShown = false;
        // Do the thing, but in reverse
        EnableChildren(false);
    }

    private void UpdateSelfTransform()
    {
        Quaternion rotate = Quaternion.Euler(-heightAngle,
                                             mainCameraTransform.rotation.eulerAngles.y,
                                             0);
        transform.rotation = rotate;
    }

    private void UpdateChildrenTransform()
    {
        UpdateChildTransform(collapseButton.transform, COLLAPSE_T);
        UpdateChildTransform(prevPageButton.transform, PREV_PAGE_T);
        //UpdateChildTransform(itemPanel.transform, ITEM_PANEL_T);
        UpdateChildTransform(nextPageButton.transform, NEXT_PAGE_T);
    }

    private void EnableChild(GameObject child, bool enabled)
    {
        child.SetActive(enabled);
    }

    private void EnableChildren(bool enabled)
    {
        EnableChild(collapseButton, enabled);   
        EnableChild(prevPageButton, enabled && !isCollapsed);
        EnableChild(itemPanel, enabled && !isCollapsed);
        EnableChild(nextPageButton, enabled && !isCollapsed);
    }

    public void Collapse()
    {
        // Disable rendering for starters
        EnableChild(prevPageButton, isCollapsed);
        EnableChild(itemPanel, isCollapsed);
        EnableChild(nextPageButton, isCollapsed);

        isCollapsed = !isCollapsed;
    }
}
