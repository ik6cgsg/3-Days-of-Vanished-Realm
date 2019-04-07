using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleObject : MonoBehaviour
{
    public void Start()
    {
        gameObject.SetActive(false);
        MagicGlassesEquipableController.glassesOn.AddListener(SetVisible);
        MagicGlassesEquipableController.glassesOff.AddListener(SetInvisible);
    }

    private void SetVisible()
    {
        gameObject.SetActive(true);
    }

    private void SetInvisible()
    {
        gameObject.SetActive(false);
    }
}
