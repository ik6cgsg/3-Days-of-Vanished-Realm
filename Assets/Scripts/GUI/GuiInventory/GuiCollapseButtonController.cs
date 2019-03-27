using UnityEngine;

public class GuiCollapseButtonController : IInteractiveObject
{
    public Texture2D collapsedTexture;
    public Texture2D uncollapsedTexture;

    private GuiInventoryController inventoryController;
    private bool isCollapsed = false;
    private Material material;
    private int texturePropertyId;

    public override void Interact()
    {
        inventoryController.Collapse();
        isCollapsed = !isCollapsed;
        if (isCollapsed)
        {
            material.SetTexture(texturePropertyId, collapsedTexture);
        }
        else
        {
            material.SetTexture(texturePropertyId, uncollapsedTexture);
        }
    }

    void Start()
    {
        material = GetComponent<Renderer>().material;
        texturePropertyId = Shader.PropertyToID("_MainTex");
        inventoryController = transform.parent.GetComponent<GuiInventoryController>();
    }

    void Update()
    {
    }
}
