using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPageInteractive : IInteractiveObject
{
    override public void Interact()
    {
        MenuController.NextPageClicked();
    }
}
