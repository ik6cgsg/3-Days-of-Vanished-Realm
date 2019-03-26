using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiNextPageButtonController : IInteractiveObject
{
    public GuiItemPanelController itemPanelController;

    public override void Interact()
    {
        itemPanelController.NextPage();
    }
}
