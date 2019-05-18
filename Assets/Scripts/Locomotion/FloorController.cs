using UnityEngine;
using UnityEngine.EventSystems;

public class FloorController : MonoBehaviour
{
    protected bool isWatched = false;

    // Public variables
    public GameObject targetCirclePrefab;
    public GameObject blackScreenPrefab;
    public AudioSource soudRef;

    public float fadeTime = 0.5F;
    public float waitTime = 0.1F;

    public float maxJumpDistance = 10;
    public float playerHeight = 2;
    public float playerEyeHeight = 1.6F;
    public float playerRadius = 0.5F;
    public Color canJumpColor = new Color(0, 1, 0, 0.30F);
    public Color cannotJumpColor = new Color(1, 0, 0, 0.30F);

    public static bool isEnabled = true;

    public static bool IsJumpingStatic { get; protected set; }

    // protected variables
    protected static GameObject targetCircle;
    protected static GameObject blackScreen;

    protected Transform playerTransform;
    protected bool canJump;
    protected bool isJumping;
    protected Vector3 jumpTarget;
    protected float timer;
    protected TargetCircleController targetCircleController;

    protected Color fadeColor = new Color(0, 0, 0, 0);

    protected Vector3 currentEuler;

    public enum JumpState
    {
        FADE_IN,
        WAIT,
        FADE_OUT
    }

    protected JumpState jumpState;
    protected int floorLayerMask;

    private void SetUpTriggers()
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
    }

    protected void SetUpFields()
    {
        // Get player transform component 
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // Create black screen and target circle game objects
        if (blackScreen == null)
        {
            blackScreen = GameObject.Instantiate(blackScreenPrefab, playerTransform);
        }
        blackScreen.GetComponent<Transform>().position = playerTransform.position;

        if (targetCircle == null)
        {
            targetCircle = GameObject.Instantiate(targetCirclePrefab, playerTransform);
        }
        targetCircleController = targetCircle.GetComponent<TargetCircleController>();

        isJumping = false;
        IsJumpingStatic = false;
        timer = 0;

        floorLayerMask = LayerMask.NameToLayer("Floor");
    }

    protected void Awake()
    {                         
        SetUpTriggers();
        SetUpFields();
    }

    protected void Update()
    {
        if (!isEnabled)
        {
            if (isWatched)
                VRCursor.SetState(VRCursor.CursorState.NEUTRAL);
            targetCircleController.EnableRenderer(false);
            return;
        }

        if (IsJumpingStatic)
        {
            VRCursor.SetState(VRCursor.CursorState.NEUTRAL);
        }

        // Continue jumping if already in progress
        if (isJumping)
        {
            Jump();
        }
        else if (isWatched)
        {
            if (!IsJumpingStatic)
            {
                targetCircleController.EnableRenderer(true);
            }
            UpdateCircleTransform();
            UpdateCanJump();
            UpdateCursor();
        }
    }

    protected bool checkCollision(Vector3 onFloorPosition)
    {
        float thresh = 0.001F;
        Vector3 p1 = onFloorPosition + new Vector3(0, playerRadius + thresh, 0);
        Vector3 p2 = onFloorPosition + new Vector3(0, playerHeight - playerRadius + thresh, 0);
        return !Physics.CheckCapsule(p1, p2, playerRadius, floorLayerMask);
    }

    virtual protected void UpdateCanJump()
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
        canJump = checkCollision(raycast.worldPosition);
    }

    virtual protected void UpdateCursor()
    {
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

    virtual protected void UpdateCircleTransform()
    {
        targetCircle.transform.position = GvrPointerInputModule.CurrentRaycastResult.worldPosition;
        targetCircle.transform.rotation = Quaternion.Euler(GvrPointerInputModule.CurrentRaycastResult.gameObject.transform.eulerAngles);
        targetCircle.transform.localScale = new Vector3(2 * playerRadius, 1, 2 * playerRadius);
    }

    protected void OnPointerEnter()
    {
        isWatched = true;
        if (!IsJumpingStatic && enabled && isEnabled)
        {
            targetCircleController.EnableRenderer(true);
            UpdateCircleTransform();
            UpdateCanJump();
            UpdateCursor();
        }
    }

    protected void OnPointerExit()
    {
        isWatched = false;
        targetCircleController.EnableRenderer(false);
    }

    protected void OnPointerClick()
    {
        if (isEnabled && canJump)
        {
            soudRef.Play();
            Jump();
        }
    }

    protected Color SetFadeColorAlpha(float alpha)
    {
        fadeColor.a = alpha;
        return fadeColor;
    }

    protected void Jump(bool setJumpTarget = true)
    {
        if (!isJumping)
        {
            Debug.Log("Start jump");
            IsJumpingStatic = true;
            isJumping = true;
            canJump = false;
            timer = 0;
            jumpState = JumpState.FADE_IN;
            if (setJumpTarget)
            {
                jumpTarget = GvrPointerInputModule.CurrentRaycastResult.worldPosition + new Vector3(0, playerEyeHeight, 0);
            }
            blackScreen.transform.localScale = new Vector3(playerRadius, playerRadius, playerRadius);
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
                soudRef.Stop();
                if (timer >= fadeTime)
                {
                    Debug.Log("Switch to no jump");
                    mtl.SetColor("_Color", SetFadeColorAlpha(0));
                    IsJumpingStatic = false;
                    isJumping = false;
                    if (isWatched)
                    {
                        UpdateCircleTransform();
                        targetCircleController.EnableRenderer(true);
                    }
                    return;
                }
                mtl.SetColor("_Color", SetFadeColorAlpha(1.0F - timer / fadeTime));
                break;
        }
    }

    protected void OnEnable()
    {
        GameObject curObject = GvrPointerInputModule.CurrentRaycastResult.gameObject;
        if (curObject != null && curObject.Equals(gameObject))
        {
            OnPointerEnter();
        }
    }
}
