using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public float moveSpeed;

    public override PlayerStateManager stateManager { get; set; }

    public override void EnterState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
        SetAnimationParameters();
    }

    public override void UpdateState()
    {
        HandleInputs();
        HandleAnimations();

        stateManager.transform.Translate(stateManager.movementDirection.normalized * stateManager.moveSpeed * Time.deltaTime);
    }

    public override void HandleInputs()
    {
        stateManager.movementDirection.x = Input.GetAxisRaw("Horizontal");
        stateManager.movementDirection.z = Input.GetAxisRaw("Vertical");

        if (Input.GetAxis("Sprint") > 0 && stateManager.healthSystem.staminaBar.fillAmount > 0)
            stateManager.SwitchState(stateManager.sprintState);

        if (Input.GetAxis("Dodge") > 0 && stateManager.healthSystem.staminaBar.fillAmount > 0)
            stateManager.SwitchState(stateManager.dodgeState);

        if (Input.GetAxis("Jump") > 0 && stateManager.healthSystem.staminaBar.fillAmount > 0)
            stateManager.SwitchState(stateManager.jumpState);

        if (Input.GetAxis("LAttack") > 0 && stateManager.healthSystem.staminaBar.fillAmount > 0)
            stateManager.SwitchState(stateManager.lAttackState);

        if (Input.GetAxis("HAttack") > 0 && stateManager.healthSystem.staminaBar.fillAmount > 0)
            stateManager.SwitchState(stateManager.hAttackState);

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            stateManager.SwitchState(stateManager.idleState);
    }

    public override void HandleAnimations()
    {
        if (stateManager.isLockedOn)
        {
            LookAtLockOnPoint();
            stateManager.animator.SetFloat("HorizontalMovement", Input.GetAxis("Horizontal"));
            stateManager.animator.SetFloat("VerticalMovement", Input.GetAxis("Vertical"));
        }
        else
        {
            LookAtMovementDirection();
            stateManager.animator.Play("Unarmed-Run-Forward");
        }
    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.SetBool("IsMoving", true);
        stateManager.animator.SetBool("IsSprinting", false);
    }

    void LookAtLockOnPoint()
    {
        stateManager.movementDirection = Quaternion.Euler(0, stateManager.animator.transform.eulerAngles.y, 0) * stateManager.movementDirection;
        stateManager.animator.transform.LookAt(Camera.main.GetComponent<CameraController>().currentLockOnPoint);
        stateManager.animator.transform.position = new Vector3(stateManager.transform.position.x, 0, stateManager.transform.position.z);
        stateManager.animator.transform.rotation = Quaternion.Euler(0, stateManager.animator.transform.rotation.eulerAngles.y, 0);
    }

    void LookAtMovementDirection()
    {
        stateManager.movementDirection = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * stateManager.movementDirection;
        Vector3 lookDirection = new Vector3(stateManager.movementDirection.x, 0.0f, stateManager.movementDirection.z);
        if (lookDirection != Vector3.zero)
            stateManager.animator.gameObject.transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    public override void OnCollisionEnter(Collider collider)
    {

    }

    public override void OnCollisionExit(Collider collider)
    {

    }
}
