using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkController : IInteractiveObject
{
    public override void Interact()
    {
        transform.Find("Rocket").gameObject.SetActive(true);
    }
}
