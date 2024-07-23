using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    Vector3 movementDirection;
    Animator animator;
    CapsuleCollider playerCollider;
    public bool isRolling = false;
    Keyboard keyboard;
    public GameObject[] lockonPoints;
    public List<GameObject> lockonPointsInView;
    public bool isLockedOn;
    public Vector3 objWorldPos;
    public GameObject currentLockon;

    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.playableGraph.Stop();
        playerCollider = GetComponentInChildren<CapsuleCollider>();
        lockonPoints = GameObject.FindGameObjectsWithTag("lockon point");
        cam1.gameObject.SetActive(true);
        cam2.gameObject.SetActive(false);
    }

    void Update()
    {
        objWorldPos = Camera.main.WorldToViewportPoint(lockonPoints[0].transform.position);
        keyboard = Keyboard.current;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        FindLockonPointsInView();

        if (isLockedOn && !isRolling)
            FaceLockon();

        if (keyboard.spaceKey.isPressed && !isRolling)
        {
            animator.playableGraph.Play();
            isRolling = true;
            if (!isLockedOn)
            {
                animator.Play("RollForward");
            }
            else
            {
                if (Input.GetKey(KeyCode.S))
                {
                    animator.Play("RollBack");
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    animator.Play("RollLeft");
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    animator.Play("RollRight");
                }
                else
                {
                    animator.Play("RollForward");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            if (!isLockedOn)
            {
                if (lockonPointsInView.Count > 0)
                {
                    currentLockon = FindClosestLockonPoint();
                    currentLockon.transform.parent.GetComponent<Renderer>().material.color = Color.red;
                    isLockedOn = true;
                    cam1.gameObject.SetActive(false);
                    cam2.gameObject.SetActive(true);
                }
            }
            else
            {
                isLockedOn = false;
                currentLockon.transform.parent.GetComponent<Renderer>().material.color = Color.white;
                currentLockon = null;
                cam1.gameObject.SetActive(true);
                cam2.gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.playableGraph.Play();
            animator.Play("PlayerAttack");
        }

        if (Input.GetKey(KeyCode.W) && !isRolling)
        {
            if (!isLockedOn)
            {
                transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
                movementDirection = transform.forward;
            }
            else
            {
                movementDirection = transform.forward;
            }
        }
        else if (Input.GetKey(KeyCode.S) && !isRolling)
        {
            if (!isLockedOn)
            {
                transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y + 180, 0);
                movementDirection = transform.forward;
            }
            else
            {
                movementDirection = -transform.forward;
            }
        }
        else
            movementDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.D) && !isRolling)
        {
            if (!isLockedOn)
            {
                if (Input.GetKey(KeyCode.W))
                    transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y + 45, 0);
                else if (Input.GetKey(KeyCode.S))
                    transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y + 135, 0);
                else
                    transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y + 90, 0);
                movementDirection = transform.forward;
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                    movementDirection = transform.right + transform.forward;
                else if (Input.GetKey(KeyCode.S))
                    movementDirection = transform.right + -transform.forward;
                else
                    movementDirection = transform.right;
            }
        }
        else if (Input.GetKey(KeyCode.A) && !isRolling)
        {
            if (!isLockedOn)
            {
                if (Input.GetKey(KeyCode.W))
                    transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y - 45, 0);
                else if (Input.GetKey(KeyCode.S))
                    transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y - 135, 0);
                else
                    transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y - 90, 0);
                movementDirection = transform.forward;
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                    movementDirection = -transform.right + transform.forward;
                else if (Input.GetKey(KeyCode.S))
                    movementDirection = -transform.right + -transform.forward;
                else
                    movementDirection = -transform.right;
            }
        }

        transform.position = new Vector3(transform.position.x + movementDirection.x * movementSpeed * Time.deltaTime,
            transform.position.y + movementDirection.y * movementSpeed * Time.deltaTime,
            transform.position.z + movementDirection.z * movementSpeed * Time.deltaTime);
    }

    void FaceLockon()
    {
        transform.LookAt(currentLockon.transform);
        Camera.main.transform.LookAt(currentLockon.transform);
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
    public void StartRoll()
    {
        playerCollider.enabled = false;
        isRolling = true;
    }

    public void StopRoll()
    {
        playerCollider.enabled = true;
        isRolling = false;
        animator.playableGraph.Stop();
    }

    public void StartAttack()
    {

    }

    public void EndAttack()
    {
        animator.playableGraph.Stop();
    }
}
