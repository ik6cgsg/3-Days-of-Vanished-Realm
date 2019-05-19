using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchEquipableController : MonoBehaviour
{
    public Transform mainCamera;
    private int maxUpAngle = 15;
    private int maxDownAngle = 70;
    private int fullRound = 360;
    private int upLimit = 270;
    private int downLimit = 90;

    void LateUpdate()
    {
        Vector3 cameraRot = mainCamera.rotation.eulerAngles;
        Quaternion localRot = transform.localRotation;
        float rollAngle = cameraRot.z;
        if (rollAngle >= 90 && rollAngle <= 270)
        {
            rollAngle -= 180;
        }
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (cameraRot.x >= fullRound - maxUpAngle || cameraRot.x <= maxDownAngle)
        {
            transform.Rotate(cameraRot.x, cameraRot.y, rollAngle);                                  
        }
        else if (cameraRot.x < fullRound - maxUpAngle && cameraRot.x >= upLimit)
        {
            transform.Rotate(fullRound - maxUpAngle, cameraRot.y, 0);
        }
        else if (cameraRot.x <= downLimit)
        {
            transform.Rotate(maxDownAngle, cameraRot.y, rollAngle);
        }
        transform.localPosition.Set(0, 0, 0);
    }
}
