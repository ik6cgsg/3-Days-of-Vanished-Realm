using UnityEngine;

public class LatticeController : IInteractiveObject
{
    public float openTime;
    public float openHeight;
    public FloorController barredFloorController;

    private bool isMoving;
    private float moveTimer;
    private bool isClosed;

    private Vector3 curPos;
    private Vector3 targetPos;

    void Start()
    {
        curPos = transform.position;
        targetPos = transform.position + new Vector3(0, openHeight, 0);
        isClosed = true;
    }

    private void Awake()
    {
        if (barredFloorController != null)
        {
            barredFloorController.enabled = false;
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

                if (barredFloorController != null)
                {
                    barredFloorController.enabled = true;
                }
            }

            transform.position = Vector3.Lerp(curPos, targetPos, moveTimer / openTime);
        }
    }

    public override void Interact()
    {
        isMoving = true;
        isClosed = false;
    }

    public override bool CanInteract()
    {
        return isClosed;
    }
}
