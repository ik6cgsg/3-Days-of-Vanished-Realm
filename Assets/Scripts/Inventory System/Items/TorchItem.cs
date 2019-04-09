using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchItem : IItem
{
    public override string Name
    {
        get
        {
            return "Tourch";
        }
    }

    public override EquipmentController.EquipableItem EquipableItem
    {
        get
        {
            return EquipmentController.EquipableItem.TORCH;
        }
    }

    private void OnEnable()
    {
        iconEquippedTexture = Resources.Load<Texture>("Textures/GUI/GuiInventory/Items/GuiTorchStopIcon");
        iconUnequippedTexture = Resources.Load<Texture>("Textures/GUI/GuiInventory/Items/GuiTorchIcon");
        IconTexture = iconUnequippedTexture;
    }

    public override bool IsEquipable()
    {
        return true;
    }
}
