using UnityEngine;

public class LeverController : IInteractiveObject
{
    public IInteractiveObject controlledObject;

    public float changeTime;
    public float startAngle;
    public float endAngle;
    public AudioSource soundRef;
    public GameObject leverGears;

    private bool isMoving;
    private float moveTimer;

    public override AudioSource Sound
    {
        get
        {
            return soundRef;
        }
    }

    private bool isPulled = false;

    public override void Save()
    {
        SaveBool("isPulled", isPulled);
    }

    public override void Load()
    {
        isPulled = LoadBool("isPulled");

        if (isPulled)
        {
            transform.localRotation = Quaternion.Euler(endAngle, 0, 0);
            leverGears.transform.localRotation = transform.localRotation;

            float tmp = startAngle;
            startAngle = endAngle;
            endAngle = tmp;
        }
    }

    void Awake()
    {
        transform.localRotation = Quaternion.Euler(startAngle, 0, 0);
        leverGears.transform.localRotation = transform.localRotation;
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

                transform.localRotation = Quaternion.Euler(endAngle, 0, 0);
                leverGears.transform.localRotation = transform.localRotation;

                float tmp = startAngle;
                startAngle = endAngle;
                endAngle = tmp;

                controlledObject.InteractWithSound();
            }

            float t = moveTimer / changeTime;
            transform.localRotation = Quaternion.Euler(startAngle * (1 - t) + endAngle * t, 0, 0);
            leverGears.transform.localRotation = transform.localRotation;
        }
    }

    public override void Interact()
    {
        isMoving = true;
        isPulled = !isPulled;
    }

    public override bool CanInteract()
    {
        return !isMoving && controlledObject.CanInteract();
    }
}
