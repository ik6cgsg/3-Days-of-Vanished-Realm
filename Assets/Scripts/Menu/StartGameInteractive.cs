using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameInteractive : IInteractiveObject
{
    public override void Interact()
    {
        MenuController.StartGameClicked();
    }
}
