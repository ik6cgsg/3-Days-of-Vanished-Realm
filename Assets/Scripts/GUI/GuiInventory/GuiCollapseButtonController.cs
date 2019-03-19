using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiCollapseButtonController : IInteractiveObject
{
    public GuiInventoryController inventoryController;

    public override void Interact()
    {
        inventoryController.Collapse();
    }

    void Start()
    {
    }

    void Update()
    {
    }
}
