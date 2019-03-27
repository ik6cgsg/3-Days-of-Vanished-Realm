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

    private Texture iconTex;

    private void OnEnable()
    {
        iconTex = Resources.Load<Texture>("Textures/GUI/GuiInventory/GuiBlankItemIcon");
    }

    public override Texture IconTexture
    {
        get
        {
            return iconTex;
        }
    }
}
