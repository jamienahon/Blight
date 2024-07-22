using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.playableGraph.Stop();
        playerCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        keyboard = Keyboard.current;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        if (keyboard.spaceKey.isPressed)
        {
            animator.playableGraph.Play();
            isRolling = true;
        }

        if (Input.GetKey(KeyCode.W) && !isRolling)
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
            movementDirection = transform.forward;
        }
        else if (Input.GetKey(KeyCode.S) && !isRolling)
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y + 180, 0);
            movementDirection = transform.forward;
        }
        else if (Input.GetKey(KeyCode.D) && !isRolling)
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y + 90, 0);
            movementDirection = transform.forward;
        }
        else if (Input.GetKey(KeyCode.A) && !isRolling)
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y + -90, 0);
            movementDirection = transform.forward;
        }
        else
            movementDirection = Vector3.zero;

        transform.position = new Vector3(transform.position.x + movementDirection.x * movementSpeed * Time.deltaTime,
            transform.position.y + movementDirection.y * movementSpeed * Time.deltaTime,
            transform.position.z + movementDirection.z * movementSpeed * Time.deltaTime);
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
}
