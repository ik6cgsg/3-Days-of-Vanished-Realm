using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrevPageInteractive : IInteractiveObject
{
    override public void Interact()
    {
        MenuController.PrevPageClicked();
    }
}
