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
        iconEquippedTexture = Texture2D.whiteTexture;
        iconUnequippedTexture = Texture2D.whiteTexture;
        IconTexture = iconUnequippedTexture;
    }
}
