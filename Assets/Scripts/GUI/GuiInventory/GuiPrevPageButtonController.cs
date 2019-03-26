using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiPrevPageButtonController : IInteractiveObject
{
    public GuiItemPanelController itemPanelController;

    public override void Interact()
    {
        itemPanelController.PrevPage();
    }
}
