using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : IItem
{
    public override string Name
    {
        get
        {
            return "Key";
        }
    }

    private void OnEnable()
    {
        iconEquippedTexture = Resources.Load<Texture>("Textures/GUI/GuiInventory/Items/GuiKeyStopIcon");
        iconUnequippedTexture = Resources.Load<Texture>("Textures/GUI/GuiInventory/Items/GuiKeyIcon");
        IconTexture = iconUnequippedTexture;
    }
}
