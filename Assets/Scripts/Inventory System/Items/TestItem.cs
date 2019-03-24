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

    public override Texture IconTexture
    {
        get
        {
            return Texture2D.whiteTexture;
        }
    }

    public override void Use()
    {
        Debug.Log("I am a Test. Guess I work.");
    }

    public override bool IsUsable()
    {
        return true;
    }
}
