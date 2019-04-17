using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadController : IInteractiveObject
{
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
    }
}
