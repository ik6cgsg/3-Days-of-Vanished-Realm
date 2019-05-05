using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portalIn;
    public Transform portalOut;
    public float offsetAngle;

    void Start()
    {
        //offsetAngle = -Vector3.Angle(portalIn.up, -playerCamera.forward);
    }

    void LateUpdate()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - portalIn.position;
        transform.position = portalOut.position - playerOffsetFromPortal;
        float angularDifferenceBetweenPortals = Quaternion.Angle(portalIn.rotation, portalOut.rotation);
        Quaternion portalRotationDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortals, Vector3.up);
        Vector3 newCameraDirection = portalRotationDifference * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
                                              transform.rotation.eulerAngles.y - 180,
                                              transform.rotation.eulerAngles.z);
                                              
    }
}
