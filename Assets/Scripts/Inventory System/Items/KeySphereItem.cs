using UnityEngine;

public class KeySphereItem : IItem
{
    public override string Name
    {
        get
        {
            return "KeySphere";
        }
    }

    private void OnEnable()
    {
        iconEquippedTexture = Resources.Load<Texture>("Textures/GUI/GuiInventory/Items/GuiKeySphereIcon");
        iconUnequippedTexture = Resources.Load<Texture>("Textures/GUI/GuiInventory/Items/GuiKeySphereIcon");
        IconTexture = iconUnequippedTexture;
    }
}
