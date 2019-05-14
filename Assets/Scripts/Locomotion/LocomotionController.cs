using UnityEngine;
using UnityEngine.EventSystems;

public class LocomotionController : FloorController
{
    public enum StepBackDirection
    {
        LINE_OF_SIGHT,
        NORMAL
    }

    public StepBackDirection stepBackDirection = StepBackDirection.NORMAL;
    public bool onlyWalls = true;

    private VRCursor cursor;
    private Transform floorTransform;
    private Vector3 onFloorPosition;
    private int wallLayerMask;

    new void Awake()
    {
        SetUpFields();
        cursor = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<VRCursor>();
        wallLayerMask = LayerMask.NameToLayer("Walls");
    }

    override protected void UpdateCircleTransform()
    {
        targetCircle.transform.position = onFloorPosition;
        targetCircle.transform.rotation = Quaternion.Euler(floorTransform.eulerAngles);
        targetCircle.transform.localScale = new Vector3(2 * playerRadius, 1, 2 * playerRadius);
    }

    override protected void UpdateCanJump()
    {
        // Check if already jumping
        if (isJumping)
        {
            canJump = false;
            return;
        }

        // Clamp at max distance
        if ((onFloorPosition - playerTransform.position).magnitude > maxJumpDistance)
        {
            canJump = false;
            return;
        }

        // Perform collision check with scene at jump target
        canJump = checkCollision(onFloorPosition);
    }

    new void Update()
    {
        // Check if already jumping globally
        if (IsJumpingStatic && !isJumping)
        {
            return;
        }

        // If already jumping, continue
        if (isJumping)
        {
            Jump();
            return;
        }

        // Check if colliding at all
        RaycastResult raycastResult = GvrPointerInputModule.CurrentRaycastResult;
        GameObject lookAtObject = raycastResult.gameObject;
        if (lookAtObject == null)
        {
            return;
        }

        // Check that not colliding with floor
        if (lookAtObject.GetComponentInChildren<FloorController>() != null
            || lookAtObject.GetComponentInParent<FloorController>() != null)
        {
            return;
        }

        // Temporarily disable circle renderer
        targetCircleController.EnableRenderer(false);

        // Check that colliding with walls if needed
        if (onlyWalls && lookAtObject.layer != wallLayerMask)
        {
            return;
        }

        // Check that not colliding with interactive object
        if (lookAtObject.GetComponentInParent<InteractiveObjectController>() != null)
        {
            return;
        }

        // Step back from hit position
        Vector3 hitPos = raycastResult.worldPosition;
        Vector3 projNorm = new Vector3(raycastResult.worldNormal.x, 0, raycastResult.worldNormal.z);
        projNorm.Normalize();
        float stepbackDist;
        switch (stepBackDirection)
        {
            case StepBackDirection.LINE_OF_SIGHT:
                // Step back along player line of sight xz-plane projection
                Vector3 projLOS = new Vector3(hitPos.x - playerTransform.position.x,
                                              0,
                                              hitPos.z - playerTransform.position.z);
                projLOS.Normalize();
                stepbackDist = (playerRadius + 0.001F) / Vector3.Dot(projNorm, -projLOS);
                jumpTarget = raycastResult.worldPosition - projLOS * stepbackDist;
                break;
            case StepBackDirection.NORMAL:
                // Step back along object normal xz-plane projection
                stepbackDist = playerRadius + 0.001F;
                jumpTarget = raycastResult.worldPosition + projNorm * stepbackDist;
                break;
        }

        // Find floor beneath
        RaycastHit floorHit = new RaycastHit();
        bool isHitFloor = Physics.Raycast(jumpTarget, -Vector3.up, out floorHit,
                                          playerHeight, -1 ^ floorLayerMask);
        if (!isHitFloor)
        {
            return;
        }

        FloorController floorController = floorHit.collider.gameObject.GetComponentInParent<FloorController>();
        if (floorController == null)
        {
            floorController = floorHit.collider.gameObject.GetComponentInChildren<FloorController>();
            if (floorController == null)
            {
                Debug.Log("U Fucked up, m8");
                return;
            }
        }
        onFloorPosition = floorHit.point;
        floorTransform = floorHit.collider.gameObject.transform;

        // Check if floor controller is enabled
        if (!floorController.enabled)
        {
            return;
        }

        // Check that colliding at acceptable height
        float hitHeight = hitPos.y - floorHit.point.y;
        if (hitHeight < 0 || hitHeight > playerHeight)
        {
            return;
        }

        // Adjust jumpTarget y-coordinate
        jumpTarget.y = floorHit.point.y + playerEyeHeight;

        UpdateCanJump();
        UpdateCursor();

        // Check if trigger down
        if (canJump && cursor.TriggerDown)
        {
            Jump(false);
            return;                
        }

        // Activate circle on floor
        UpdateCircleTransform();
        targetCircleController.EnableRenderer(true);
    }
}
