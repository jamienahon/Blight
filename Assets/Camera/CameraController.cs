using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public PlayerStateManager stateManager;
    public Transform currentLockOnPoint;
    public Animator cameraAnim;
    public CinemachineVirtualCamera lockOnCam;
    public CinemachineFreeLook followCam;
    public GameObject cameraTarget;
    public bool isClicking = false;

    [Header("Settings")]
    [SerializeField] float noticeZone = 10;
    [Tooltip("Angle_Degree")] [SerializeField] float maxNoticeAngle = 60;

    [SerializeField] LayerMask targetLayers;

    public Canvas lockOnCanvas;
    public float lockOnScale;

    private void Update()
    {
        if (Input.GetAxis("Lockon") > 0 && !isClicking)
        {
            isClicking = true;
            if (stateManager.isLockedOn)
            {
                //If there is already a target, Reset.
                EndLockOn();
                stateManager.isLockedOn = false;
                return;
            }

            currentLockOnPoint = ScanNearBy();
            if (currentLockOnPoint)
                StartLockOn();
            else
                EndLockOn();
        }

        if (stateManager.isLockedOn)
        {
            if (!LockOnPointInRange())
                EndLockOn();
        }

        if (Input.GetAxis("Lockon") == 0)
            isClicking = false;

        if (stateManager.isLockedOn)
        {
            cameraTarget.transform.LookAt(currentLockOnPoint);
            lockOnCanvas.transform.position = currentLockOnPoint.position;
            lockOnCanvas.transform.localScale = Vector3.one * ((lockOnCam.transform.position - currentLockOnPoint.transform.position).magnitude * lockOnScale);

        }
    }

    public void EndLockOn()
    {
        lockOnCanvas.gameObject.SetActive(false);
        currentLockOnPoint = null;
        stateManager.isLockedOn = false;
        stateManager.SwitchState(stateManager.idleState);
        followCam.m_XAxis.Value = stateManager.animator.transform.rotation.eulerAngles.y;
        followCam.m_YAxis.Value = 0.5f;
        cameraAnim.Play("FollowCamera");
    }

    private Transform ScanNearBy()
    {
        Collider[] nearbyTargets = Physics.OverlapSphere(stateManager.transform.position, noticeZone, targetLayers);
        float closestAngle = maxNoticeAngle;
        Transform closestTarget = null;
        if (nearbyTargets.Length <= 0) return null;

        for (int i = 0; i < nearbyTargets.Length; i++)
        {
            Vector3 dir = nearbyTargets[i].transform.position - Camera.main.transform.position;
            dir.y = 0;
            float angle = Vector3.Angle(Camera.main.transform.forward, dir);

            if (angle < closestAngle)
            {
                closestTarget = nearbyTargets[i].transform;
                closestAngle = angle;
            }
        }
        return closestTarget;
    }

    void StartLockOn()
    {
        lockOnCanvas.gameObject.SetActive(true);
        lockOnCam.LookAt = currentLockOnPoint;
        cameraAnim.Play("TargetCamera");
        stateManager.isLockedOn = true;
    }

    bool LockOnPointInRange()
    {
        float dis = (stateManager.transform.position - currentLockOnPoint.position).magnitude;
        if (dis / 2 > noticeZone) return false; else return true;
    }
}
