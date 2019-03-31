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

    void Start()
    {
        gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        Vector3 cameraRot = mainCamera.rotation.eulerAngles;
        // Debug.Log(cameraRot);
        if (cameraRot.x >= fullRound - maxUpAngle || cameraRot.x <= maxDownAngle)
        {
            transform.rotation = Quaternion.Euler(cameraRot.x, cameraRot.y, 0);
        }
        else if (cameraRot.x < fullRound - maxUpAngle && cameraRot.x >= upLimit)
        {
            transform.rotation = Quaternion.Euler(fullRound - maxUpAngle, cameraRot.y, 0);
        }
        else if (cameraRot.x <= downLimit)
        {
            transform.rotation = Quaternion.Euler(maxDownAngle, cameraRot.y, 0);
        }
    }
}
