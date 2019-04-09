using UnityEngine;

public class TestItem : IItem
{
    public override string Name
    {
        get
        {
            return "TestItem";
        }
    }

    private void OnEnable()
    {
        iconEquippedTexture = Texture2D.whiteTexture;
        iconUnequippedTexture = Texture2D.whiteTexture;
        IconTexture = iconUnequippedTexture;
    }

    public override bool IsEquipable()
    {
        return true;
    }
}
