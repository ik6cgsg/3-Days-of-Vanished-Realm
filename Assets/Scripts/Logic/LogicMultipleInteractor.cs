public class LogicMultipleInteractor : IInteractiveObject
{
    public IInteractiveObject[] interactees;

    public override void Interact()
    {
        foreach (IInteractiveObject interactiveObject in interactees)
        {
            interactiveObject.InteractWithSound();
        }
    }

    public override bool CanInteract()
    {
        foreach (IInteractiveObject interactee in interactees)
        {
            if (!interactee.CanInteract())
            {
                return false;
            }
        }

        return true;
    }
}
