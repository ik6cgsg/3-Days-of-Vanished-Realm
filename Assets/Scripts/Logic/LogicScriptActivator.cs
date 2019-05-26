using UnityEngine;

public class LogicScriptActivator : IInteractiveObject
{
    public MonoBehaviour script;

    public override void Interact()
    {
        script.enabled = !script.enabled;
    }
}
