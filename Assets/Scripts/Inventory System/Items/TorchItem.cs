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
        iconEquippedTexture = Texture2D.blackTexture;
        iconUnequippedTexture = Texture2D.blackTexture;
        IconTexture = iconUnequippedTexture;
    }

    public override bool IsEquipable()
    {
        return true;
    }
}
