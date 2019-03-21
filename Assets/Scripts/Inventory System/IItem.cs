using UnityEngine;
using UnityEngine.EventSystems;

public abstract class IItem : ScriptableObject
{
    public abstract string Name
    {
        get;
    }

    public abstract Texture IconTexture
    {
        get;
    }

    // Can we use current item
    public virtual bool IsUsable()
    {
        return false;
    }

    // Action function of current item
    public virtual void Use()
    {

    }
}
