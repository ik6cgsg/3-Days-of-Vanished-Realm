using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MagicGlassesEquipableController : MonoBehaviour
{
    public static UnityEvent magicGlassesOn;
    public static UnityEvent magicGlassesOff;

    public Transform mainCamera;
    private Material equippedMaterial;

    public void Start()
    {
        magicGlassesOn = new UnityEvent();
        magicGlassesOff = new UnityEvent();

        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        magicGlassesOn.Invoke();
    }

    public void OnDisable()
    {
        magicGlassesOff.Invoke();
    }

    public void LateUpdate()
    {
        transform.position = mainCamera.position;
    }
}
