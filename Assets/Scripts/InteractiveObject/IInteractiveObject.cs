using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IInteractiveObject : MonoBehaviour
{
    public virtual AudioSource Sound
    {
        get
        {
            return null;
        }
    }

    public void InteractGlobal()
    {
        if (Sound != null)
        {
            Sound.Play();
        }
        Interact();
    }

    public abstract void Interact();

    public virtual bool CanInteract()
    {
        return true;
    }
}
