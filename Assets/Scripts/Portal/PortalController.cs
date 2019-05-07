using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class PortalController : MonoBehaviour
{
    public Camera portalView;
    public PortalController portalOut;

    void Start()
    {
        portalOut.portalView.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer child in children)
        {
            if (child.name.Equals("RenderPlane"))
            {
                child.material.mainTexture = portalOut.portalView.targetTexture;
                break;
            }
        }
    }

    void LateUpdate()
    {
        // Position
        Vector3 lookerPosition = portalOut.transform.worldToLocalMatrix.MultiplyPoint3x4(Camera.main.transform.position);
        lookerPosition = new Vector3(-lookerPosition.x, lookerPosition.y, -lookerPosition.z);
        portalView.transform.localPosition = lookerPosition;
        // Rotation
        Quaternion difference = transform.rotation * Quaternion.Inverse(portalOut.transform.rotation * Quaternion.Euler(0, -180, 0));
        portalView.transform.rotation = difference * Camera.main.transform.rotation;
        // Clipping
        //portalView.nearClipPlane = lookerPosition.magnitude;
    }
}
