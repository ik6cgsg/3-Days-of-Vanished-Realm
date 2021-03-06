﻿using UnityEngine;

public class ChestController : IInteractiveObject
{
    public float changeTime;
    public float startAngle;
    public float endAngle;
    public AudioSource soundRef;

    private bool isMoving;
    private float moveTimer;

    private bool isOpen = false;

    public override void Save()
    {
        SaveBool("isOpen", isOpen);
    }

    public override void Load()
    {
        isOpen = LoadBool("isOpen");

        if (isOpen)
        {
            Rotate(endAngle);

            float tmp = startAngle;
            startAngle = endAngle;
            endAngle = tmp;
        }
    }

    public override AudioSource Sound
    {
        get
        {
            return soundRef;
        }
    }

    public override float SoundPlayTime
    {
        get
        {
            return changeTime;
        }
    }

    public enum RotationAxis
    {
        X,
        Z
    }

    public RotationAxis rotationAxis;

    private void Rotate(float angle)
    {
        switch (rotationAxis)
        {
            case RotationAxis.X:
                transform.localRotation = Quaternion.Euler(angle, 0, 0);
                break;
            case RotationAxis.Z:
                transform.localRotation = Quaternion.Euler(0, 0, angle);
                break;
        }
    }

    void Awake()
    {
        Rotate(startAngle);
    }

    void Update()
    {
        if (isMoving)
        {
            moveTimer += Time.deltaTime;

            if (moveTimer >= changeTime)
            {
                moveTimer = 0;
                isMoving = false;

                Rotate(endAngle);

                float tmp = startAngle;
                startAngle = endAngle;
                endAngle = tmp;
            }

            float t = moveTimer / changeTime;
            Rotate(startAngle * (1 - t) + endAngle * t);
        }
    }

    public override void Interact()
    {
        isMoving = true;
        isOpen = !isOpen;
    }

    public override bool CanInteract()
    {
        return true;
    }
}
