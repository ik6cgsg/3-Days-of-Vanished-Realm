using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameInteractive : IInteractiveObject
{
    override public void Interact()
    {
        MenuController.NewGameClicked();
    }
}
