using UnityEngine;

public class ChestController : IInteractiveObject
{
    public float changeTime;
    public float startAngle;
    public float endAngle;

    private bool isMoving;
    private float moveTimer;

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

    void Start()
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
    }

    public override bool CanInteract()
    {
        return true;
    }
}
