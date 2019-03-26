using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : IItem
{
    private string name;
    // Start is called before the first frame update
    void Start()
    {
        name = "Key";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override string Name
    {
        get
        {
            return name;
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
