public class LogicBoolean : IInteractiveObject
{
    public bool Value { get; private set; }

    public override void Interact()
    {
        Value = !Value;    
    }
}
