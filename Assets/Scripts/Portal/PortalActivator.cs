using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalActivator : IInteractiveObject
{
    public AudioSource soundRef = null;
    public BasePortal baseRef;

    public override AudioSource Sound
    {
        get
        {
            return soundRef;
        }
    }

    public override void Interact()
    {
        baseRef.SetActive(true);
    }
}
