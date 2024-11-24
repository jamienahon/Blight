
using UnityEngine;


public class PlayerHeavyShootState : PlayerState
{
    public override PlayerStateManager stateManager { get; set; }
    public AudioClip heavyShootSound;
    public bool loopSound;
    public bool moveBack;

    public override void EnterState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
        HandleAnimations();
        SetAnimationParameters();
        HandleAudio();
        LookAtCameraDirection();

        stateManager.animator.SetLayerWeight(1, 0);
    }

    public override void UpdateState()
    {
        if (moveBack)
        {
            stateManager.transform.Translate(-stateManager.animator.transform.forward * stateManager.heavyAttackMoveSpeed * Time.deltaTime);
        }
    }

    public override void HandleInputs()
    {

    }

    public override void HandleAnimations()
    {

    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.speed = 1;
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
