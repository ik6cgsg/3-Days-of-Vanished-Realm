using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : IInteractiveObject
{
    public float changeTime;
    public float startAngle;
    public float endAngle;

    private bool isMoving;
    private float moveTimer;

    private Vector3 rotateCentre;

    private void Start()
    {
        rotateCentre = transform.position;
        rotateCentre.z -= transform.localScale.z * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            float dt = Time.deltaTime;
            moveTimer += dt;

            if (moveTimer >= changeTime)
            {
                moveTimer = 0;
                isMoving = false;

                float tmp = startAngle;
                startAngle = endAngle;
                endAngle = tmp;

            }

            float ang = (endAngle - startAngle) * dt;
            transform.RotateAround(rotateCentre, new Vector3(1, 0, 0), ang);
        }
    }

    public override void Interact()
    {
        isMoving = true;
    }

    public override bool CanInteract()
    {
        return true;
    }
}
