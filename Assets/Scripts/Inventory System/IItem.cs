using UnityEngine;
using UnityEngine.EventSystems;

public abstract class IItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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

    public virtual void OnPointerClick(PointerEventData pointerEventData)
    {

    }

    public virtual void OnPointerEnter(PointerEventData pointerEventData)
    {

    }

    public virtual void OnPointerExit(PointerEventData pointerEventData)
    {

    }
}
