using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePortal : IInteractiveObject
{
    public Transform portalOut;
    public bool isActive;
    public GameObject blackScreenPrefab;
    public AudioSource soundRef = null;

    private Transform player;
    private bool isJumping;

    private float angleTarget;
    private Vector3 jumpTarget;

    private float timer;
    private float fadeTime = 0.5F;
    private float waitTime = 0.2F;
    private float playerHeight = 2;
    private float playerEyeHeight = 1.6F;
    private float playerRadius = 0.5F;
    private FloorController.JumpState jumpState;
    private Color fadeColor = new Color(0, 0, 0, 0);
    private static GameObject blackScreen;

    public override AudioSource Sound
    {
        get
        {
            return soundRef;
        }
    }

    public override void Interact()
    {
        Vector3 offset = player.position - gameObject.transform.position;
        offset.Set(offset.x, 0, offset.z);
        Vector3 newPos = portalOut.position + portalOut.forward * offset.magnitude + Vector3.up * playerEyeHeight;
        Vector3 camFwd = Camera.main.transform.forward;
        camFwd.y = 0;
        float dirCam = Vector3.Cross(camFwd, portalOut.forward).normalized.y;
        float angleCam = dirCam * Vector3.Angle(camFwd, portalOut.forward);
        jumpTarget = newPos;
        angleTarget = angleCam;
        Jump();
    }

    public void SetActive(bool flag)
    {
        isActive = flag;
        transform.Find("GameObject/RenderPlane").gameObject.SetActive(isActive);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.Find("GameObject/RenderPlane").gameObject.SetActive(isActive);
        blackScreen = GameObject.Instantiate(blackScreenPrefab, player.transform);
        blackScreen.transform.position = player.transform.position;
        isJumping = false;
    }

    private void Update()
    {
        if (isJumping)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (!isJumping)
        {
            Debug.Log("Start jump");
            isJumping = true;
            timer = 0;
            jumpState = FloorController.JumpState.FADE_IN;
            blackScreen.transform.localScale = new Vector3(playerRadius, playerRadius, playerRadius);
            return;
        }

        Material mtl = blackScreen.GetComponent<Renderer>().material;

        timer += Time.deltaTime;

        switch (jumpState)
        {
            case FloorController.JumpState.FADE_IN:
                // Fade to black
                if (timer >= fadeTime)
                {
                    Debug.Log("Switch to wait");
                    jumpState = FloorController.JumpState.WAIT;
                    timer = 0;
                    mtl.SetColor("_Color", SetFadeColorAlpha(1));
                    return;
                }
                fadeColor.a = 1;
                mtl.SetColor("_Color", SetFadeColorAlpha(timer / fadeTime));
                break;
            case FloorController.JumpState.WAIT:
                // Wait with black screen                
                if (timer >= waitTime)
                {
                    Debug.Log("Switch to fade out");
                    jumpState = FloorController.JumpState.FADE_OUT;
                    player.position = jumpTarget;
                    blackScreen.transform.position = jumpTarget;
                    player.Rotate(0, angleTarget, 0);
                    timer = 0;
                    return;
                }
                break;
            case FloorController.JumpState.FADE_OUT:
                // Fade out of black
                if (timer >= fadeTime)
                {
                    Debug.Log("Switch to no jump");
                    mtl.SetColor("_Color", SetFadeColorAlpha(0));
                    isJumping = false;
                    return;
                }
                mtl.SetColor("_Color", SetFadeColorAlpha(1.0F - timer / fadeTime));
                break;
        }
    }

    private Color SetFadeColorAlpha(float alpha)
    {
        fadeColor.a = alpha;
        return fadeColor;
    }
}
