using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoorController : IInteractiveObject
{
    public float openTime;
    public float openHeight;
    public AudioSource soundRef;

    private bool isMoving;
    private float moveTimer;

    private Vector3 curPos;
    private Vector3 targetPos;

    public override AudioSource Sound
    {
        get
        {
            return soundRef;
        }
    }

    public override float SoundPlayTime
    {
        get
        {
            return openTime;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        curPos = transform.position;
        targetPos = transform.position + new Vector3(0, openHeight, 0);
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
            }

            transform.position = Vector3.Lerp(curPos, targetPos, moveTimer / openTime);
        }
    }

    public override void Interact()
    {
        isMoving = true;
        Destroy(GetComponent<InteractiveObjectController>());
        VRCursor.SetState(VRCursor.CursorState.NEUTRAL);
        InventoryController.RemoveItem("Key");
    }

    public override bool CanInteract()
    {
        return InventoryController.HasItem("Key");
    }
}
