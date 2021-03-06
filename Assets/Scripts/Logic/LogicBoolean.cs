﻿public class LogicBoolean : IInteractiveObject
{
    public bool Value;

    public override void Save()
    {
        SaveBool("Value", Value);
    }

    public override void Load()
    {
        Value = LoadBool("Value");
    }

    public override void Interact()
    {
        Value = !Value;
    }
}
