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
        Texture iconTex = Resources.Load<Texture>("Textures/Items/MagicGlassesTex");
        iconEquippedTexture = iconTex;
        iconUnequippedTexture = iconTex;
        IconTexture = iconUnequippedTexture;
    }

    public override bool IsEquipable()
    {
        return true;
    }
}
