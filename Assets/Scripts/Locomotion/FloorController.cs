using UnityEngine;
using UnityEngine.EventSystems;

public class FloorController : MonoBehaviour
{
    private bool isWatched = false;

    // Public variables
    public GameObject targetCircle;
    public GameObject blackScreen;

    public float fadeTime = 0.5F;
    public float waitTime = 0.1F;

    public float maxJumpDistance = 10;
    public float playerHeight = 2;
    public float playerEyeHeight = 1.6F;
    public float playerRadius = 0.5F;
    public Color canJumpColor = new Color(0, 1, 0, 0.30F);
    public Color cannotJumpColor = new Color(1, 0, 0, 0.30F);

    // Private variables
    private Transform playerTransform;
    private bool canJump;
    private bool isJumping;
    private Vector3 jumpTarget;
    private float timer;
    private TargetCircleController targetCircleController;

    private Color fadeColor = new Color(0, 0, 0, 0);

    private enum JumpState
    {
        FADE_IN,
        WAIT,
        FADE_OUT
    }

    JumpState jumpState;

    private void Start()
    {
        // Add EventTrigger components
        EventTrigger trigger = gameObject.AddComponent(typeof(EventTrigger)) as EventTrigger;

        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((eventData) => { OnPointerEnter(); });

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((eventData) => { OnPointerExit(); });

        EventTrigger.Entry entryClick = new EventTrigger.Entry();
        entryClick.eventID = EventTriggerType.PointerClick;
        entryClick.callback.AddListener((eventData) => { OnPointerClick(); });

        trigger.triggers.Add(entryEnter);
        trigger.triggers.Add(entryExit);
        trigger.triggers.Add(entryClick);

        // Get player transform component 
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // Create black screen and target circle game objects
        blackScreen = GameObject.Instantiate(blackScreen, playerTransform);
        blackScreen.GetComponent<Transform>().position = playerTransform.position;
        targetCircle = GameObject.Instantiate(targetCircle, playerTransform);
        targetCircleController = targetCircle.GetComponent<TargetCircleController>();

        isJumping = false;
        timer = 0;
    }

    private void Update()
    {
        // Continue jumping if already in progress
        if (isJumping)
        {
            Jump();
        }

        if (isWatched)
        {
            UpdateCircleTransform();
            UpdateCanJump();
            if (canJump)
            {
                VRCursor.SetState(VRCursor.CursorState.CAN_STEP);
                targetCircleController.SetColor(canJumpColor);
            }
            else
            {
                VRCursor.SetState(VRCursor.CursorState.CANNOT_STEP);
                targetCircleController.SetColor(cannotJumpColor);
            }
        }
    }

    private void UpdateCanJump()
    {
        // Check if already jumping
        if (isJumping)
        {
            canJump = false;
            return;
        }

        RaycastResult raycast = GvrPointerInputModule.CurrentRaycastResult;

        // Clamp at max distance
        if (raycast.distance > maxJumpDistance)
        {
            canJump = false;
            return;
        }

        // Perform collision check with scene at jump target
        Vector3 lookAt = raycast.worldPosition;
        float thresh = 0.001F;
        Vector3 p1 = lookAt + new Vector3(0, playerRadius + thresh, 0);
        Vector3 p2 = lookAt + new Vector3(0, playerHeight - playerRadius + thresh, 0);
        canJump = !Physics.CheckCapsule(p1, p2, playerRadius);
    }

    private void UpdateCircleTransform()
    {
        //circle.transform.position = Vector3.Lerp(circle.transform.position, GvrPointerInputModule.CurrentRaycastResult.worldPosition, 1000 * Time.deltaTime);
        //targetCircle.transform.position = Vector3.Lerp(targetCircle.transform.position, GvrPointerInputModule.CurrentRaycastResult.worldPosition + new Vector3(0, 1, 0), Time.deltaTime / (Time.deltaTime + 0.30F));
        targetCircle.transform.position = GvrPointerInputModule.CurrentRaycastResult.worldPosition + new Vector3(0, 0.001F, 0);
        targetCircle.transform.localScale = new Vector3(2 * playerRadius, 1, 2 * playerRadius);
    }

    private void OnPointerEnter()
    {
        isWatched = true;
        targetCircleController.EnableRenderer(true);
    }

    private void OnPointerExit()
    {
        isWatched = false;
        targetCircleController.EnableRenderer(false);
    }

    private void OnPointerClick()
    {
        if (canJump)
        {
            Jump();
        }
    }

    private Color SetFadeColorAlpha(float alpha)
    {
        fadeColor.a = alpha;
        return fadeColor;
    }

    private void Jump()
    {
        if (isJumping == false)
        {
            Debug.Log("Start jump");
            isJumping = true;
            canJump = false;
            timer = 0;
            jumpState = JumpState.FADE_IN;
            jumpTarget = GvrPointerInputModule.CurrentRaycastResult.worldPosition + new Vector3(0, playerEyeHeight, 0);
            blackScreen.transform.localScale = new Vector3(2 * playerRadius, 2 * playerRadius, 2 * playerRadius);
            targetCircleController.EnableRenderer(false);
            return;
        }

        Material mtl = blackScreen.GetComponent<Renderer>().material;

        timer += Time.deltaTime;

        switch (jumpState)
        {
            case JumpState.FADE_IN:
                // Fade to black
                if (timer >= fadeTime)
                {
                    Debug.Log("Switch to wait");
                    jumpState = JumpState.WAIT;
                    timer = 0;
                    mtl.SetColor("_Color", SetFadeColorAlpha(1));
                    return;
                }
                fadeColor.a = 1;
                mtl.SetColor("_Color", SetFadeColorAlpha(timer / fadeTime));
                break;
            case JumpState.WAIT:
                // Wait with black screen
                playerTransform.position = jumpTarget;
                blackScreen.transform.position = jumpTarget;
                if (timer >= waitTime)
                {
                    Debug.Log("Switch to fade out");
                    jumpState = JumpState.FADE_OUT;
                    timer = 0;
                    return;
                }
                break;
            case JumpState.FADE_OUT:
                // Fade out of black
                if (timer >= fadeTime)
                {
                    Debug.Log("Switch to no jump");
                    mtl.SetColor("_Color", SetFadeColorAlpha(0));
                    isJumping = false;
                    if (isWatched)
                        targetCircleController.EnableRenderer(true);
                    return;
                }
                mtl.SetColor("_Color", SetFadeColorAlpha(1.0F - timer / fadeTime));
                break;
        }
    }
}
