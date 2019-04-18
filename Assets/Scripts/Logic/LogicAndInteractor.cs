using UnityEngine;

public class LogicAndInteractor : MonoBehaviour
{
    public LogicBoolean[] logicBooleans;
    public IInteractiveObject interactee;

    private bool hasInteracted;

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

        interactee.Interact();
        hasInteracted = true;
    }
}
