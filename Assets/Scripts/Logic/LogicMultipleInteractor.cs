public class LogicMultipleInteractor : IInteractiveObject
{
    public IInteractiveObject[] interactees;

    public override void Interact()
    {
        foreach (IInteractiveObject interactiveObject in interactees)
        {
            interactiveObject.Interact();
        }
    }
}
