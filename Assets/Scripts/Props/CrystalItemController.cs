﻿using UnityEngine;

public class CrystalItemController : MonoBehaviour
{
    public float amplitude;
    public float offset;
    public float bobSpeed;
    public float rotationSpeed;

    void Update()
    {
        transform.localPosition = new Vector3(0, amplitude * Mathf.Sin(Time.time + offset), 0);
        transform.localRotation = Quaternion.Euler(rotationSpeed * (Time.time + offset),
                                                   2 * rotationSpeed * (Time.time + offset),
                                                   -0.5F * rotationSpeed * (Time.time + offset));
    }
}
