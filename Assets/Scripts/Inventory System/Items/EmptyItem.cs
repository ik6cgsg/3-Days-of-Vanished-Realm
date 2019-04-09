using UnityEngine;

public class EmptyItem : IItem
{
    public override string Name
    {
        get
        {
            return "EmptyItem";
        }
    }

    private void OnEnable()
    {
        Texture iconTex = Resources.Load<Texture>("Textures/GUI/GuiInventory/GuiBlankItemIcon");
        iconEquippedTexture = iconTex;
        iconUnequippedTexture = iconTex;
        IconTexture = iconUnequippedTexture;
    }
}
