using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portalIn;
    public Transform portalOut;
    public Material cameraMaterial;

    private Vector3 forward;

    void Start()
    {
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        GetComponent<Camera>().targetTexture = rt;
        cameraMaterial.mainTexture = rt;
    }

    void Update()
    {
        //transform.localPosition = UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.CenterEye);
        //transform.localRotation = UnityEngine.XR.InputTracking.GetLocalRotation(UnityEngine.XR.XRNode.CenterEye);
        Vector3 playerOffsetFromPortal = playerCamera.position - portalIn.position;
        transform.position = portalOut.position - playerOffsetFromPortal;
        float angularDifferenceBetweenPortals = Quaternion.Angle(portalIn.rotation, portalOut.rotation);
        Quaternion portalRotationDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortals, Vector3.up);
        Vector3 newCameraDirection = portalRotationDifference * playerCamera.forward;
        
        //transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        transform.rotation = Quaternion.Euler(playerCamera.rotation.eulerAngles.x,
                                              playerCamera.rotation.eulerAngles.y - 180,
                                              playerCamera.rotation.eulerAngles.z);
        
        //transform.localPosition = transform.localPosition + UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.CenterEye);
    }
}
