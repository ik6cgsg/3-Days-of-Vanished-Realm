using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatticeController : IInteractiveObject
{
    public float openTime;
    public float openHeight;
    public FloorController barredFloorController;

    private bool isMoving;
    private float moveTimer;
    private bool state;

    private Vector3 curPos;
    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        curPos = transform.position;
        targetPos = transform.position + new Vector3(0, openHeight, 0);
        state = true;
    }

    private void Awake()
    {
        barredFloorController.enabled = false;
    }

    // Update is called once per frame
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

                if (!state)
                    barredFloorController.enabled = true;
            }

            transform.position = Vector3.Lerp(curPos, targetPos, moveTimer / openTime);
        }
    }

    public override void Interact()
    {
        isMoving = true;
        state = !state;
    }

    public override bool CanInteract()
    {
        return state;
    }
}
