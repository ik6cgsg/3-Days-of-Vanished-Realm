using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VRCursor : GvrBasePointer
{
    // Minimum distance of the reticle (in meters).
    public const float RETICLE_DISTANCE_MIN = 0.45f;

    // Current state of cursor
    public enum CursorState
    {
        NEUTRAL,
        CAN_INTERACT,
        CANNOT_INTERACT,
        TOO_FAR,
        CAN_STEP,
        CANNOT_STEP,
        NUM_OF_STATES,   // please, if you would add new states,
        NONE             // put them before NUM_OF_STATES
    }

    // Maximum distance of the cursor raycasting (in meters).
    public float maxReticleDistance = 20.0f;

    // Radius of entering detecting intersection with objects.
    public float enterRadius = 0.5f;

    // Radius of stopping detecting intersection with objects.
    public float exitRadius = 0.5f;

    // Speed of cursor rotation
    public float rotationSpeed = 45.0f;

    // Width of cursor
    public float width = 0.05f;

    // Sorting order to use for the reticle's renderer.
    // Range values come from https://docs.unity3d.com/ScriptReference/Renderer-sortingOrder.html.
    // Default value 32767 ensures gaze reticle is always rendered on top.
    [Range(-32767, 32767)]
    public int sortingOrder = 32767;

    // Prebuilded cursor mesh
    private Mesh mesh;

    // Current rotation angle of triangle
    private float curRotAngle = 0.0f;

    // Textures
    [SerializeField]
    private Texture neutralTex;
    [SerializeField]
    private Texture canInteractTex;
    [SerializeField]
    private Texture cannotInteractTex;
    [SerializeField]
    private Texture tooFarTex;
    [SerializeField]
    private Texture canStepTex;
    [SerializeField]
    private Texture cannotStepTex;

    // Array of references to above state textures
    private static Texture[] stateTextures;

    // Current distance of the reticle (in meters).
    public float ReticleDistanceInMeters
    {
        get;
        private set;
    }

    // Returns the max distance from the pointer that raycast hits will be detected.
    public override float MaxPointerDistance
    {
        get
        {
            return maxReticleDistance;
        }
    }

    // Current state of cursor
    private static CursorState CurrentState
    {
        get;
        set;
    }

    // The material used to render the reticle.
    private static Material MaterialComp
    {
        get;
        set;
    }

    // Start is called before the first frame update.
    protected override void Start()
    {
        base.Start();

        Renderer rendComponent = GetComponent<Renderer>();
        rendComponent.sortingOrder = sortingOrder;
        MaterialComp = rendComponent.material;
        CurrentState = CursorState.NONE;

        BuildMesh();
        InitTextures();
        SetState(CursorState.NEUTRAL);
    }

    // Initing the array of textures references
    private void InitTextures()
    {
        stateTextures = new Texture[(int)CursorState.NUM_OF_STATES];
        stateTextures[(int)CursorState.NEUTRAL] = neutralTex;
        stateTextures[(int)CursorState.CAN_INTERACT] = canInteractTex;
        stateTextures[(int)CursorState.CANNOT_INTERACT] = cannotInteractTex;
        stateTextures[(int)CursorState.TOO_FAR] = tooFarTex;
        stateTextures[(int)CursorState.CAN_STEP] = canStepTex;
        stateTextures[(int)CursorState.CANNOT_STEP] = cannotStepTex;
    }

    // Building meshes
    private void BuildMesh()
    {
        gameObject.AddComponent<MeshFilter>();
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        #region Mesh Build
        Vector3[] verts = new Vector3[4];
        verts[0] = new Vector3(-width, -width, 1.0f);
        verts[1] = new Vector3(-width, width, 1.0f);
        verts[2] = new Vector3(width, width, 1.0f);
        verts[3] = new Vector3(width, -width, 1.0f);

        int[] indices = new int[6];

        indices[0] = 0;
        indices[1] = 1;
        indices[2] = 3;
        indices[3] = 1;
        indices[4] = 2;
        indices[5] = 3;

        Vector2[] texCoords = new Vector2[4];

        texCoords[0] = new Vector2(0, 0);
        texCoords[1] = new Vector2(0, 1);
        texCoords[2] = new Vector2(1, 1);
        texCoords[3] = new Vector2(1, 0);

        mesh.vertices = verts;
        mesh.triangles = indices;
        mesh.uv = texCoords;
        mesh.RecalculateBounds();
        #endregion
    }

    // Setting current state of cursor
    public static void SetState(CursorState newState)
    {
        if (CurrentState == newState)
        {
            return;
        }

        CurrentState = newState;
        MaterialComp.SetTexture("_MainTex", stateTextures[(int)CurrentState]);
    }

    // Update is called once per frame.
    void Update()
    {
        ReticleDistanceInMeters = Mathf.Clamp(ReticleDistanceInMeters, RETICLE_DISTANCE_MIN, maxReticleDistance);
        MaterialComp.SetFloat("_DistanceInMeters", ReticleDistanceInMeters);

        HandleRotation();
    }

    // Rotate the cursor, if it is pointing to interactive object
    private void HandleRotation()
    {
        if (CurrentState == CursorState.CAN_INTERACT)
        {
            curRotAngle += Time.deltaTime * rotationSpeed;
            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), Time.deltaTime * rotationSpeed);
        }
        else
        {
            curRotAngle = curRotAngle % 360;
            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), -curRotAngle);
            curRotAngle = 0.0f;
        }
    }

    // Called when the pointer is facing a valid GameObject.
    public override void OnPointerEnter(RaycastResult raycastResult, bool isInteractive)
    {
        Vector3 targetLocalPosition = base.PointerTransform.InverseTransformPoint(raycastResult.worldPosition);
        ReticleDistanceInMeters = Mathf.Clamp(targetLocalPosition.z, RETICLE_DISTANCE_MIN, maxReticleDistance);
    }

    // Called every frame the user is still pointing at a valid GameObject.
    public override void OnPointerHover(RaycastResult raycastResult, bool isInteractive)
    {
        Vector3 targetLocalPosition = base.PointerTransform.InverseTransformPoint(raycastResult.worldPosition);
        ReticleDistanceInMeters = Mathf.Clamp(targetLocalPosition.z, RETICLE_DISTANCE_MIN, maxReticleDistance);
    }

    // Called when the pointer no longer faces an object previously intersected with a ray projected from the camera.
    public override void OnPointerExit(GameObject previousObject)
    {
        ReticleDistanceInMeters = maxReticleDistance;
        SetState(CursorState.NEUTRAL);
    }

    // Called when a click is initiated.
    public override void OnPointerClickDown()
    {
    }

    // Called when click is finished.
    public override void OnPointerClickUp()
    {
    }

    // Return the radius of the pointer.
    public override void GetPointerRadius(out float enterRadius, out float exitRadius)
    {
        enterRadius = this.enterRadius;
        exitRadius = this.exitRadius;
    }

    public override bool TriggerDown
    {
        get
        {
            return base.TriggerDown || MagnetTrigger.TriggerDown;
        }
    }
}
