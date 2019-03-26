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

    public override Texture IconTexture
    {
        get
        {
            return Texture2D.whiteTexture;
        }
    }
}
