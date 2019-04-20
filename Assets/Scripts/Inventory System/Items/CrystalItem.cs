using UnityEngine;

public class CrystalItem : IItem
{
    public override string Name
    {
        get
        {
            return "Crystal";
        }
    }

    private void OnEnable()
    {
        iconEquippedTexture = Resources.Load<Texture>("Textures/GUI/GuiInventory/Items/GuiCrystalIcon");
        iconUnequippedTexture = Resources.Load<Texture>("Textures/GUI/GuiInventory/Items/GuiCrystalIcon");
        IconTexture = iconUnequippedTexture;
    }
}
