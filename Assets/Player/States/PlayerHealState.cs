using UnityEngine;

public class PlayerHealState : PlayerState
{
    public AudioClip walkingSound;
    public bool loopSound;
    float endHeal;

    public override PlayerStateManager stateManager { get; set; }

    public override void EnterState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
        HandleAnimations();
        SetAnimationParameters();
        HandleAudio();

        endHeal = Time.time + stateManager.healLengthSeconds;
    }

    public override void UpdateState()
    {
        HandleInputs();

        if (stateManager.isLockedOn)
        {
            LookAtLockOnPoint();
            stateManager.animator.SetFloat("HorizontalMovement", 0);
            stateManager.animator.SetFloat("VerticalMovement", 0);
        }
        else
        {
            LookAtMovementDirection();
        }


        stateManager.transform.Translate(stateManager.movementDirection.normalized * stateManager.healingMoveSpeed * Time.deltaTime);

        if (Time.time >= endHeal)
            stateManager.SwitchState(stateManager.idleState);
    }

    public override void HandleInputs()
    {
        stateManager.movementDirection.x = Input.GetAxisRaw("Horizontal");
        stateManager.movementDirection.z = Input.GetAxisRaw("Vertical");
    }

    public override void HandleAnimations()
    {
        stateManager.animator.Play("Healing");
    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.speed = 0.5f;
        stateManager.animator.SetBool("IsMoving", false);
        stateManager.animator.SetBool("IsSprinting", false);
    }

    public override void HandleAudio()
    {
        stateManager.playerAudio.clip = walkingSound;
        stateManager.playerAudio.loop = loopSound;
        stateManager.playerAudio.Play();
    }

    void LookAtLockOnPoint()
    {
        stateManager.movementDirection = Quaternion.Euler(0, stateManager.animator.transform.eulerAngles.y, 0) * stateManager.movementDirection;
        stateManager.animator.transform.LookAt(Camera.main.GetComponent<CameraController>().currentLockOnPoint);
        //stateManager.animator.transform.position = new Vector3(stateManager.transform.position.x, 0, stateManager.transform.position.z);
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