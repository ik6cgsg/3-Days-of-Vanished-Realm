using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchItem : IItem
{
    public override string Name
    {
        get
        {
            return "Tourch";
        }
    }

    public override Texture IconTexture
    {
        get
        {
            return Texture2D.blackTexture;
        }
    }

    public override void Use()
    {
        Debug.Log("I am a torch.");
        GameObject torch = TorchUsableController.Instance;
        bool status = torch.activeSelf; 
        torch.SetActive(!status);
    }

    public override bool IsUsable()
    {
        return true;
    }
}
