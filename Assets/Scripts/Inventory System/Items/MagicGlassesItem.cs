using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicGlassesItem : IItem
{
    public override string Name
    {
        get
        {
            return "MagicGlasses";
        }
    }

    public override EquipmentController.EquipableItem EquipableItem
    {
        get
        {
            return EquipmentController.EquipableItem.MAGIC_GLASSES;
        }
    }

    private void OnEnable()
    {
        iconEquippedTexture = Resources.Load<Texture>("Textures/GUI/GuiInventory/Items/GuiGlassesStopIcon");
        iconUnequippedTexture = Resources.Load<Texture>("Textures/GUI/GuiInventory/Items/GuiGlassesIcon");
        IconTexture = iconUnequippedTexture;
    }

    public override bool IsEquipable()
    {
        return true;
    }
}
