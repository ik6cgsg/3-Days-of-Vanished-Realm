using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoDoorController : IInteractiveObject
{
    public float openTime;
    public float openHeight;

    private bool isMoving;
    private float moveTimer;

    private Vector3 curPos;
    private Vector3 targetPos;

    private void Start()
    {
        curPos = transform.position;
        targetPos = transform.position + new Vector3(0, openHeight, 0);
    }

    public override void Interact()
    {
        if (!isMoving)
        {
            isMoving = true;
        }
    }

    void Update()
    {
        if (isMoving)
        {
            moveTimer += Time.deltaTime;

            if (moveTimer >= openTime)
            {
                moveTimer = 0;

                isMoving = false;

                transform.position = targetPos;
                Vector3 tmp = targetPos;
                targetPos = curPos;
                curPos = tmp;
            }

            transform.position = Vector3.Lerp(curPos, targetPos, moveTimer / openTime);
        }
    }
}
