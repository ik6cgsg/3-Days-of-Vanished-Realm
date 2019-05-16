using UnityEngine;

public class CrystalItemController : MonoBehaviour
{
    public float amplitude;
    public float bobSpeed;
    public float rotationSpeed;

    void Update()
    {
        transform.localPosition = new Vector3(0, amplitude * Mathf.Sin(Time.time), 0);
        transform.localRotation = Quaternion.Euler(rotationSpeed * Time.time,
                                                   2 * rotationSpeed * Time.time,
                                                   -0.5F * rotationSpeed * Time.time);
    }
}
