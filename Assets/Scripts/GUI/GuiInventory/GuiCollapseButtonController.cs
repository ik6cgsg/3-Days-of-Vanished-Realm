public class GuiCollapseButtonController : IInteractiveObject
{
    private GuiInventoryController inventoryController;

    public override void Interact()
    {
        inventoryController.Collapse();
    }

    void Start()
    {
        inventoryController = transform.parent.GetComponent<GuiInventoryController>();
    }

    void Update()
    {
    }
}
