using UnityEngine;

public class InvisibleObjectController : MonoBehaviour
{
    Transform[] children;


    private static void SetEnabled(Transform transform, bool enabled)
    {
        if (transform == null)
        {
            return;
        }

        Renderer renderer = transform.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = enabled;
        }

        Collider collider = transform.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = enabled;
        }

        Canvas canvas = transform.GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.enabled = enabled;
        }
    }

    private void SetChildrenEnabled(bool enabled)
    {
        foreach (Transform child in children)
        {
            SetEnabled(child, enabled);
        }
    }

    public void Awake()
    {
        children = GetComponentsInChildren<Transform>(true);

        SetChildrenEnabled(false);
        MagicGlassesEquipableController.glassesOn.AddListener(SetVisible);
        MagicGlassesEquipableController.glassesOff.AddListener(SetInvisible);
    }

    private void SetVisible()
    {
        SetChildrenEnabled(true);
    }

    private void SetInvisible()
    {
        SetChildrenEnabled(false);
    }
}
