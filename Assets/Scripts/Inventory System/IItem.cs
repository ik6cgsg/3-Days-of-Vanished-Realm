using UnityEngine;

public abstract class IItem : ScriptableObject
{
    [SerializeField]
    protected Texture iconEquippedTexture;
    [SerializeField]
    protected Texture iconUnequippedTexture;

    public virtual EquipmentController.EquipableItem EquipableItem
    {
        get
        {
            return EquipmentController.EquipableItem.NONE;
        }
    }

    public abstract string Name
    {
        get;
    }

    public Texture IconTexture
    {
        get;
        set;
    }

    public virtual bool IsEquipable()
    {
        return false;
    }

    public void Equip()
    {
        IconTexture = iconEquippedTexture;
    }

    public void Unequip()
    {
        IconTexture = iconUnequippedTexture;
    }
}
