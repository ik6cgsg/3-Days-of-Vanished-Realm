using UnityEngine;

public class ChestController : IInteractiveObject
{
    public float changeTime;
    public float startAngle;
    public float endAngle;

    private bool isMoving;
    private float moveTimer;

    void Start()
    {
        transform.localRotation = Quaternion.Euler(0, 0, startAngle);
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

                transform.localRotation = Quaternion.Euler(0, 0, endAngle);

                float tmp = startAngle;
                startAngle = endAngle;
                endAngle = tmp;
            }

            float t = moveTimer / changeTime;
            transform.localRotation = Quaternion.Euler(0, 0, startAngle * (1 - t) + endAngle * t);
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
