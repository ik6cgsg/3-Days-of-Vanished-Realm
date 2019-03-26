using UnityEngine;

public class GuiItemIconController : IInteractiveObject
{
    public int itemInventoryIndex;

    private IItem item;
    private Material material;
    private int texturePropertyId;

    public override void Interact()
    {
        item.Use();
    }

    public override bool CanInteract()
    {
        return item.IsUsable();
    }

    void Awake()
    {
        material = GetComponent<Renderer>().material;
        texturePropertyId = Shader.PropertyToID("_MainTex");
    }

    void Update()
    {
    }

    public void SetItem(IItem newItem)
    {
        item = newItem;
        // Update texture
        material.SetTexture(texturePropertyId, item.IconTexture);
    }
}
