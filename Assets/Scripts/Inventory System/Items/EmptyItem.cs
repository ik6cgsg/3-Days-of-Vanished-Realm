using UnityEngine;

public class EmptyItem : IItem
{
    public override string Name
    {
        get
        {
            return "Empty";
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
