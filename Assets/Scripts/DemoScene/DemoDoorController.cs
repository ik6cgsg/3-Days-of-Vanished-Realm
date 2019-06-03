using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoDoorController : IInteractiveObject
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

    private bool isOpen = false;

    public override void Save()
    {
        SaveBool("isOpen", isOpen);
    }

    public override void Load()
    {
        isOpen = LoadBool("isOpen");

        if (isOpen)
        {
            transform.position = targetPos;
            Vector3 tmp = targetPos;
            targetPos = curPos;
            curPos = tmp;
        }
    }

    private void Awake()
    {
        curPos = transform.position;
        targetPos = transform.position + new Vector3(0, openHeight, 0);
    }

    public override bool CanInteract()
    {
        return !isOpen;
    }

    public override void Interact()
    {
        if (!isMoving)
        {
            isMoving = true;
        }

        isOpen = !isOpen;
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
