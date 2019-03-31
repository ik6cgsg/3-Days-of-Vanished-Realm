using UnityEngine;
using UnityEngine.Events;

public class MagicGlassesEquipableController : MonoBehaviour
{
    public static UnityEvent magicGlassesOn = new UnityEvent();
    public static UnityEvent magicGlassesOff = new UnityEvent();

    // public Transform mainCamera;

    private Material equippedMaterial;

    public void Start()
    {
        // gameObject.SetActive(false);
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
        // transform.position = mainCamera.position;
    }
}
