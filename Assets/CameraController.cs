using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public PlayerStateManager stateManager;
    public GameObject[] lockonPoints;
    public List<GameObject> lockonPointsInView = new List<GameObject>();
    public GameObject lockonPoint;
    public Animator cameraAnim;
    public CinemachineVirtualCamera lockOnCam;
    public CinemachineFreeLook followCam;

    private void Update()
    {
        FindLockonPointsInView();
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            if (!stateManager.isLockedOn)
            {
                lockonPoint = FindClosestLockonPoint();
                if (lockonPoint != null)
                {
                    stateManager.isLockedOn = true;
                    lockOnCam.LookAt = lockonPoint.transform;
                    cameraAnim.Play("TargetCamera");
                }
            }
            else
            {
                lockonPoint = null;
                stateManager.isLockedOn = false;
                cameraAnim.Play("FollowCamera");
            }
        }
    }

    void FindLockonPointsInView()
    {
        foreach (GameObject point in lockonPoints)
        {
            if (Camera.main.WorldToViewportPoint(point.transform.position).z > 1)
            {
                if (!lockonPointsInView.Contains(point))
                    lockonPointsInView.Add(point);
            }
            else
            {
                if (lockonPointsInView.Contains(point))
                    lockonPointsInView.Remove(point);
            }
        }
    }

    GameObject FindClosestLockonPoint()
    {
        GameObject closestPoint = lockonPoints[0];
        foreach (GameObject point in lockonPoints)
        {
            if (FindDistanceFromCenterOfCamera(point) < FindDistanceFromCenterOfCamera(closestPoint))
                closestPoint = point;
        }
        return closestPoint;
    }

    float FindDistanceFromCenterOfCamera(GameObject obj)
    {
        Vector3 worldPos = Camera.main.WorldToViewportPoint(obj.transform.position);
        worldPos = new Vector3(worldPos.x - 0.5f, worldPos.y - 0.5f, 0);
        return worldPos.magnitude;
    }
}
