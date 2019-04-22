using UnityEngine;

public class LeverController : IInteractiveObject
{
    public IInteractiveObject controlledObject;

    public float changeTime;
    public float startAngle;
    public float endAngle;
    public AudioSource soundRef;

    public GameObject leverGears;
    public AudioSource soundRef;

    private bool isMoving;
    private float moveTimer;

    public override AudioSource Sound
    {
        get
        {
            return soundRef;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = Quaternion.Euler(startAngle, 0, 0);
        leverGears.transform.localRotation = transform.localRotation;
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

                transform.localRotation = Quaternion.Euler(endAngle, 0, 0);
                leverGears.transform.localRotation = transform.localRotation;

                float tmp = startAngle;
                startAngle = endAngle;
                endAngle = tmp;

                controlledObject.InteractGlobal();
            }

            float t = moveTimer / changeTime;
            transform.localRotation = Quaternion.Euler(startAngle * (1 - t) + endAngle * t, 0, 0);
            leverGears.transform.localRotation = transform.localRotation;
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
