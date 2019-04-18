using UnityEngine;
using UnityEngine.Events;

public class MagicGlassesEquipableController : MonoBehaviour
{
    public static UnityEvent glassesOn = new UnityEvent();
    public static UnityEvent glassesOff = new UnityEvent();

    // public Transform mainCamera;

    private Material equippedMaterial;

    public void Start()
    {
        // gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        glassesOn.Invoke();
        FloorController.isEnabled = false;
    }

    public void OnDisable()
    {
        glassesOff.Invoke();
        FloorController.isEnabled = true;
    }

    public void LateUpdate()
    {
        // transform.position = mainCamera.position;
    }
}
