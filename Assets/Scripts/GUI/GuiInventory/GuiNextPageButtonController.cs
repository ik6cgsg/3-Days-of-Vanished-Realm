using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiNextPageButtonController : IInteractiveObject
{
    public GuiItemPanelController itemPanelController;
    public AudioSource soundRef;

    public override AudioSource Sound
    {
        get
        {
            return soundRef;
        }
    }

    public override void Interact()
    {
        itemPanelController.NextPage();
    }
}
