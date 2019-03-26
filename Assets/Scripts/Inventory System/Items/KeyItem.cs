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

    public override Texture IconTexture
    {
        get
        {
            return Texture2D.whiteTexture;
        }
    }
}
