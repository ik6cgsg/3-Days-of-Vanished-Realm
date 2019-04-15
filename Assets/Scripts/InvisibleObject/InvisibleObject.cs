using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleObject : MonoBehaviour
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
            transform.GetComponent<Renderer>().enabled = enabled;
        }

        Collider collider = transform.GetComponent<Collider>();
        if (collider != null)
        {
            transform.GetComponent<Collider>().enabled = enabled;
        }
    }

    private void SetChildrenEnabled(bool enabled)
    {
        foreach (Transform child in children)
        {
            SetEnabled(child, enabled);
        }
    }

    public void Start()
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
