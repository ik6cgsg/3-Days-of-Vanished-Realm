using UnityEngine;

public class ButtonController : IInteractiveObject
{
    public IInteractiveObject controlledObject;

    public float moveTime;
    public float amplitude;

    private bool isMoving;
    private float moveTimer;

    void Update()
    {
        if (isMoving)
        {
            moveTimer += Time.deltaTime;

            if (moveTimer >= moveTime)
            {
                moveTimer = 0;
                isMoving = false;

                transform.localPosition = Vector3.zero;

                controlledObject.Interact();
            }

            float t = moveTimer / moveTime;
            transform.localPosition = new Vector3(0, 0, amplitude * Mathf.Sin(t * Mathf.PI));
        }
    }

    public override void Interact()
    {
        isMoving = true;
    }

    public override bool CanInteract()
    {
        return !isMoving && controlledObject.CanInteract();
    }
}
