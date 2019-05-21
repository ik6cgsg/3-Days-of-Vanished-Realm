using UnityEngine;

public class LogicAndInteractor : ISavableObject
{
    public LogicBoolean[] logicBooleans;
    public IInteractiveObject interactee;

    private bool hasInteracted;

    public override void Save()
    {
        SaveBool("hasInteracted", hasInteracted);
    }

    public override void Load()
    {
        hasInteracted = LoadBool("hasInteracted");
    }

    void Update()
    {
        if (hasInteracted)
        {
            return;
        }

        foreach (LogicBoolean logicBoolean in logicBooleans)
        {
            if (!logicBoolean.Value)
            {
                return;
            }
        }

        interactee.InteractWithSound();
        hasInteracted = true;
    }
}
