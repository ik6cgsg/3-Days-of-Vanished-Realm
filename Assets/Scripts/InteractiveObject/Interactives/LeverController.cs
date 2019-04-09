using UnityEngine;

public class LeverController : IInteractiveObject
{
    public IInteractiveObject controlledObject;

    public float changeTime;
    public float startAngle;
    public float endAngle;

    private bool isMoving;
    private float moveTimer;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(startAngle, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            moveTimer += Time.deltaTime;

            if (moveTimer >= changeTime)
            {
                moveTimer = 0;
                isMoving = false;

                transform.rotation = Quaternion.Euler(endAngle, 0, 0);

                float tmp = startAngle;
                startAngle = endAngle;
                endAngle = tmp;
            }

            float t = moveTimer / changeTime;
            transform.rotation = Quaternion.Euler(startAngle * (1 - t) + endAngle * t, 0, 0);
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
