using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IInteractiveObject : MonoBehaviour
{
    public abstract void Interact();

    public virtual bool CanInteract()
    {
        return true;
    }
}
