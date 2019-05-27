using UnityEngine;

public class LogicScriptActivator : IInteractiveObject
{
    public MonoBehaviour script;

    public override void Save()
    {
        SaveBool("enabled", script.enabled);
    }

    public override void Load()
    {
        script.enabled = LoadBool("enabled");
    }

    public override void Interact()
    {
        script.enabled = !script.enabled;
    }
}
