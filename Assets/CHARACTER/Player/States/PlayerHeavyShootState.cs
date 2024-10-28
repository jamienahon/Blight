
using UnityEngine;


public class PlayerHeavyShootState : PlayerState
{
    public override PlayerStateManager stateManager { get; set; }
    public AudioClip heavyShootSound;
    public bool loopSound;

    public override void EnterState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
        HandleAnimations();
        SetAnimationParameters();
        HandleAudio();
        LookAtCameraDirection();

        stateManager.animator.SetLayerWeight(1, 1);
    }

    public override void UpdateState()
    {
        if (stateManager.allowMovementWhileAttacking)
        {
            HandleInputs();

            if (stateManager.isLockedOn)
                stateManager.movementDirection = Quaternion.Euler(0, stateManager.animator.transform.eulerAngles.y, 0) * stateManager.movementDirection;
            else
                stateManager.movementDirection = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * stateManager.movementDirection;

            stateManager.transform.Translate(stateManager.movementDirection.normalized * stateManager.attackMovementSpeed * Time.deltaTime);
        }
    }

    public override void HandleInputs()
    {
        stateManager.movementDirection.x = Input.GetAxisRaw("Horizontal");
        stateManager.movementDirection.z = Input.GetAxisRaw("Vertical");
        stateManager.animator.SetFloat("HorizontalMovement", Input.GetAxis("Horizontal"));
        stateManager.animator.SetFloat("VerticalMovement", Input.GetAxis("Vertical"));

        if (stateManager.movementDirection.x == 0 && stateManager.movementDirection.z == 0)
            stateManager.animator.SetBool("IsMoving", false);
        else
            stateManager.animator.SetBool("IsMoving", true);
    }

    public override void HandleAnimations()
    {

    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.SetBool("IsMoving", false);
        stateManager.animator.SetBool("IsSprinting", false);
        stateManager.animator.SetBool("IsDodging", false);
        stateManager.animator.SetBool("IsHeavyAttacking", true);
        stateManager.animator.SetFloat("HorizontalMovement", 0);
        stateManager.animator.SetFloat("VerticalMovement", 0);
    }

    public override void HandleAudio()
    {

    }

    void LookAtCameraDirection()
    {
        stateManager.animator.transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
    }

   

    public override void OnCollisionEnter(Collider collider)
    {

    }

    public override void OnCollisionExit(Collider collider)
    {

    }
}
