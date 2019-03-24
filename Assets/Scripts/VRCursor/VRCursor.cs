﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VRCursor : GvrBasePointer
{
    // Minimum distance of the reticle (in meters).
    public const float RETICLE_DISTANCE_MIN = 0.45f;

    // Current type of cursor
    public enum CursorType
    {
        CT_NONE,
        CT_TRIANGLE,    // 'triangle' state
        CT_CROSS,       // 'cross' state
    }

    // Maximum distance of the cursor raycasting (in meters).
    public float maxReticleDistance = 20.0f;

    // Radius of entering detecting intersection with objects.
    public float enterRadius = 0.5f;

    // Radius of stopping detecting intersection with objects.
    public float exitRadius = 0.5f;
    
    // Growth speed multiplier for the reticle
    public float growthSpeed = 8.0f;

    // Length of reticle triangle in disactive mode
    public float minTriRad = 0.1f;

    // Length of reticle triangle in active mode
    public float maxTriRad = 0.2f;

    // Cross mesh height
    public float crossHeight = 0.1f;

    // Cross color
    public Color crossColor = new Color(0, 0, 1, 0.5f);

    // Triangle color
    public Color triangleColor = new Color(1, 1, 0, 0.5f);

    // Sorting order to use for the reticle's renderer.
    // Default value 32767 ensures gaze reticle is always rendered on top.
    [Range(-32767, 32767)]
    public int sortingOrder = 32767;

    // Speed of triangle rotation
    public float rotationSpeed = 45.0f;

    // Time settings for 'cross' figure of cursor
    private float crossTime = 0.2f;
    private float crossTimer = 0.0f;

    // Prebuilded triangle mesh
    private Mesh triangleMesh;

    // Current rotation angle of triangle
    private float curRotAngle = 0.0f;

    // The material used to render the reticle.
    public Material MaterialComp
    {
        private get;
        set;
    }

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

    // <summary> Current state of cursor </summary>
    private CursorType CurrentType
    {
        get;
        set;
    }

    protected override void Start()
    {
        base.Start();

        Renderer rendComponent = GetComponent<Renderer>();
        rendComponent.sortingOrder = sortingOrder;
        MaterialComp = rendComponent.material;

        BuildMesh();
        SetCurrentMesh(CursorType.CT_TRIANGLE);
    }

    private void BuildMesh()
    {
        gameObject.AddComponent<MeshFilter>();
        triangleMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = triangleMesh;

        #region Tringle Mesh Build
        Vector3[] triVerts = new Vector3[3];
        triVerts[0] = new Vector3(0.05f * Mathf.Cos(Mathf.PI / 6), -0.05f * Mathf.Sin(Mathf.PI / 2), 1.0f);
        triVerts[1] = new Vector3(-0.05f * Mathf.Cos(Mathf.PI / 6), -0.05f * Mathf.Sin(Mathf.PI / 2), 1.0f);
        triVerts[2] = new Vector3(0.0f, 0.03f, 1.0f);

        int[] triIndices = new int[3];

        triIndices[0] = 0;
        triIndices[1] = 1;
        triIndices[2] = 2;

        triangleMesh.vertices = triVerts;
        triangleMesh.triangles = triIndices;
        triangleMesh.RecalculateBounds();
        #endregion
    }

    // Setting current mesh to MeshFilter component
    public void SetCurrentMesh(CursorType newType)
    {
        if (CurrentType == newType)
        {
            return;
        }

        CurrentType = newType;
        if (CurrentType == CursorType.CT_CROSS)
        {
            MaterialComp.SetColor("_Color", crossColor);
        }
        else
        {
            MaterialComp.SetColor("_Color", triangleColor);
        }
    }

    // Handle the cross figure of cursor
    private void HandleCrossState()
    {
        if (crossTimer >= crossTime)
        {
            crossTimer = 0.0f;
            SetCurrentMesh(CursorType.CT_TRIANGLE);
            return;
        }

        crossTimer += Time.deltaTime;
    }

    void Update()
    {
        ReticleDistanceInMeters = Mathf.Clamp(ReticleDistanceInMeters, RETICLE_DISTANCE_MIN, maxReticleDistance);
        MaterialComp.SetFloat("_DistanceInMeters", ReticleDistanceInMeters);

        // Cross state handle
        if (CurrentType == CursorType.CT_CROSS)
        {
            HandleCrossState();
        }
    }

    // Rotate the triangle cursor, if it is pointing to interactive object
    private void HandleTriangleRotation(bool isInteractive)
    {
        if (CurrentType == CursorType.CT_CROSS)
        {
            return;
        }

        if (isInteractive)
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

        HandleTriangleRotation(isInteractive);
    }

    // Called every frame the user is still pointing at a valid GameObject.
    public override void OnPointerHover(RaycastResult raycastResult, bool isInteractive)
    {
        Vector3 targetLocalPosition = base.PointerTransform.InverseTransformPoint(raycastResult.worldPosition);
        ReticleDistanceInMeters = Mathf.Clamp(targetLocalPosition.z, RETICLE_DISTANCE_MIN, maxReticleDistance);

        HandleTriangleRotation(isInteractive);
    }

    // Called when the pointer no longer faces an object previously intersected with a ray projected from the camera.
    public override void OnPointerExit(GameObject previousObject)
    {
        ReticleDistanceInMeters = maxReticleDistance;

        HandleTriangleRotation(false);
    }

    public override void OnPointerClickDown()
    {
    }

    public override void OnPointerClickUp()
    {
    }

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
