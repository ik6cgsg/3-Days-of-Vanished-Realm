using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : ISavableObject
{
    // Angle threshold
    public const float START_ROTATION_ANGLE_THRESHOLD_IN_DEGREES = 33f;
    public const float FINISH_ROTATION_ANGLE_THRESHOLD_IN_DEGREES = 18f;

    static private Vector3 START_CAMERA_FORWARD = new Vector3(0, 0, 1);

    // Static array of menu pages
    static public List<GameObject> staticPages = new List<GameObject>();

    // Array of mene pages from UI
    public GameObject[] pages;

    static private Vector3 curPageForward;

    // Transform of camera
    public Transform mainCamera;
    static public Transform staticMainCamera;

    public float distanceToCamera;
    public float height;
    public float rightOffset;

    static private float staticDistanceToCamera;
    static private float staticHeight;
    static private float staticRightOffset;

    // Index of current opened page
    static private int currentPage = -1;

    public float angleStepInDegrees = 0.01f;
    static private float staticAngleStep;

    private bool needRotation = false;

    void Awake()
    {
        staticDistanceToCamera = distanceToCamera;
        staticHeight = height;
        staticRightOffset = rightOffset;

        staticAngleStep = angleStepInDegrees * Mathf.Deg2Rad;

        staticMainCamera = mainCamera;

        if (pages.Length > 0)
        {
            foreach (GameObject obj in pages)
            {
                staticPages.Add(obj);
            }

            ShowPage(0);
        }
    }

    private void Update()
    {
        Vector3 up = new Vector3(0, 1, 0);
        Vector3 cameraForward = GetCameraForward();

        Vector3 curPagePos = (staticMainCamera.position + staticDistanceToCamera * curPageForward +
            staticHeight * up).normalized;

        Vector3 shouldPos = (staticMainCamera.position + staticDistanceToCamera * cameraForward +
            staticHeight * up).normalized;

        float angle = Mathf.Acos(Vector3.Dot(curPagePos, shouldPos));

        if (Mathf.Abs(angle) * Mathf.Rad2Deg < FINISH_ROTATION_ANGLE_THRESHOLD_IN_DEGREES)
        {
            needRotation = false;
            return;
        }

        if (!needRotation && Mathf.Abs(angle) * Mathf.Rad2Deg > START_ROTATION_ANGLE_THRESHOLD_IN_DEGREES)
        {
            needRotation = true;
        }

        if (needRotation)
        {
            bool sign = Vector3.Cross(curPagePos, shouldPos).y > 0;
            staticAngleStep = sign ? Mathf.Abs(staticAngleStep) : -Mathf.Abs(staticAngleStep);

            Vector3 newPageForward = new Vector3(
                curPageForward.x * Mathf.Cos(angle * staticAngleStep) + curPageForward.z * Mathf.Sin(angle * staticAngleStep),
                0,
                -curPageForward.x * Mathf.Sin(angle * staticAngleStep) + curPageForward.z * Mathf.Cos(angle * staticAngleStep));
            newPageForward.Normalize();
            curPageForward = newPageForward;

            Vector3 newPagePos = staticMainCamera.position + staticDistanceToCamera * newPageForward +
                staticHeight * up - staticRightOffset * staticMainCamera.right;

            for (int i = 0; i < staticPages.Count; i++)
            {
                staticPages[i].transform.position = newPagePos;
                staticPages[i].transform.RotateAroundLocal(up, angle * staticAngleStep);
            }
        }
    }

    static public void NewGameClicked()
    {
        ShowPage(1);
    }

    static public void ContinueClicked()
    {
        //TODO: ...
    }

    static public void PrevPageClicked()
    {
        ShowPage(currentPage - 1);
    }

    static public void NextPageClicked()
    {
        ShowPage(currentPage + 1);
    }

    static public void StartGameClicked()
    {
        SaveGlobalString("currentScene", "HubLevel");
        FindObjectOfType<SceneManagerController>().LoadScene("HubLevel");
    }

    static private void SetCurrentPage(int index)
    {
        currentPage = index;
    }

    static private void ShowPage(int index)
    {
        if (currentPage >= 0)
        {
            staticPages[currentPage].SetActive(false);
        }

        SetCurrentPage(index);
        staticPages[currentPage].SetActive(true);
        CorrectStartPosition();
    }

    static private void CorrectStartPosition()
    {
        curPageForward = GetCameraForward();

        Vector3 up = new Vector3(0, 1, 0);
        for (int i = 0; i < staticPages.Count; i++)
        {
            staticPages[i].transform.position = staticMainCamera.position + staticDistanceToCamera * curPageForward +
               staticHeight * up;
        }
    }

    static private Vector3 GetCameraForward()
    {
        Vector3 cameraForward = staticMainCamera.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 x = new Vector3(1, 0, 0);
        x.x = cameraForward.x < 0 ? -1 : 1;
        Vector3 z = new Vector3(0, 0, 1);
        z.z = cameraForward.z < 0 ? -1 : 1;

        cameraForward = cameraForward * Vector3.Dot(cameraForward, x) + cameraForward * Vector3.Dot(cameraForward, z);
        cameraForward.Normalize();

        return cameraForward;
    }
}
