using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCircleController : MonoBehaviour
{
    private GameObject circle;
    private Vector3 yAxis;

    public float rotationSpeed = 45;


    private void Start()
    {
        // Find circle in children
        foreach (Transform child in transform)
        {
            circle = child.gameObject;
        }

        yAxis = new Vector3(0.0F, -1.0F, 0.0F);
    }

    private void Update()
    {
        circle.transform.Rotate(yAxis, Time.deltaTime * rotationSpeed);
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
