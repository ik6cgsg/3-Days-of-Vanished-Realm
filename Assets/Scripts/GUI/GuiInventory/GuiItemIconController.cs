﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiItemIconController : IInteractiveObject
{
    public int itemInventoryIndex;

    private IItem item;

    public override void Interact()
    {
        Debug.Log("I was pressed");
        item.Use();
    }

    public override bool CanInteract()
    {
        return true;// item.IsUsable();
    }

    void Awake()
    {
        SetItem((IItem)ScriptableObject.CreateInstance("EmptyItem"));
    }

    void Update()
    {
    }

    public void SetItem(IItem newItem)
    {
        item = newItem;
        // Update texture      
    }
}
