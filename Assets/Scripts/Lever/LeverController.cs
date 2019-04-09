using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : IInteractiveObject
{
    public IInteractiveObject controlledObject;

    public float changeTime;
    public float startAngle;
    public float endAngle;

    private bool isMoving;
    private float moveTimer;

    private Vector3 rotateCentre;
   
    // Start is called before the first frame update
    void Start()
    {
        float h = transform.localScale.y;
        rotateCentre = transform.position;
        rotateCentre.y -= h * Mathf.Cos(startAngle);
        rotateCentre.z -= h * Mathf.Sin(startAngle);

        transform.rotation = Quaternion.Euler(startAngle, 0, 0);
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
        controlledObject.Interact();
        isMoving = true;
    }

    public override bool CanInteract()
    {
        return controlledObject.CanInteract();
    }
}
