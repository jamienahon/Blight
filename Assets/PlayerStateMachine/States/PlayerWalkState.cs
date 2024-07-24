using UnityEngine;

public class PlayerWalkState : PlayerState
{
    Vector3 movementDirection = new Vector3();

    public override void EnterState(PlayerStateManager stateManager)
    {
        stateManager.animator.SetBool("IsMoving", true);
    }

    public override void UpdateState(PlayerStateManager stateManager)
    {
        HandleInputs(stateManager);
        HandleAnimations(stateManager);
        stateManager.transform.Translate(movementDirection.normalized * stateManager.moveSpeed * Time.deltaTime);
    }

    public override void HandleInputs(PlayerStateManager stateManager)
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.z = Input.GetAxisRaw("Vertical");

        if (Input.GetAxis("Dodge") > 0)
            stateManager.SwitchState(stateManager.dodgeState);

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            stateManager.SwitchState(stateManager.idleState);
    }

    public override void HandleAnimations(PlayerStateManager stateManager)
    {
        if (stateManager.isLockedOn && stateManager.currentState == this)
        {
            stateManager.animator.SetFloat("HorizontalMovement", movementDirection.x);
            stateManager.animator.SetFloat("VerticalMovement", movementDirection.z);

            movementDirection = Quaternion.Euler(0, stateManager.animator.transform.eulerAngles.y, 0) * movementDirection;
            stateManager.animator.transform.LookAt(GameObject.Find("Cam").GetComponent<CameraController>().lockonPoint.transform);
            stateManager.animator.transform.position = new Vector3(stateManager.transform.position.x, 0, stateManager.transform.position.z);
            stateManager.animator.transform.rotation = Quaternion.Euler(0, stateManager.animator.transform.rotation.eulerAngles.y, 0);
        }
        else
        {
            stateManager.animator.Play("Unarmed-Run-Forward");

            movementDirection = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * movementDirection;
            Vector3 movement = new Vector3(movementDirection.x, 0.0f, movementDirection.z);
            if (movement != Vector3.zero)
                stateManager.animator.gameObject.transform.rotation = Quaternion.LookRotation(movement);
        }
    }

    public override void OnCollisionEnter(PlayerStateManager stateManager, Collision collision)
    {

    }

    public override void OnCollisionExit(PlayerStateManager stateManager, Collision collision)
    {

    }
}
