using UnityEngine;

public class TargetCircleController : MonoBehaviour
{
    private GameObject circle;
    private float currentAngle;

    public float rotationSpeed = 45;


    private void Start()
    {
        // Find circle in children
        foreach (Transform child in transform)
        {
            circle = child.gameObject;
        }
    }

    private void Update()
    {
        currentAngle += -Time.deltaTime * rotationSpeed;
        if (currentAngle < -360)
        {
            currentAngle = 360 + currentAngle;
        }

        circle.transform.localRotation = Quaternion.Euler(0,
            Camera.main.transform.rotation.eulerAngles.y + currentAngle, 0);
    }

    public void SetColor(Color color)
    {
        circle.GetComponent<Renderer>().material.SetColor("_Color", color);
    }

    public void EnableRenderer(bool enabled)
    {
        if (circle != null)
        {
            circle.GetComponent<Renderer>().enabled = enabled;
        }
    }
}
