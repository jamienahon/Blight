using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShootState : PlayerState
{
    public override PlayerStateManager stateManager { get; set; }
    public AudioClip shootSound;
    public bool loopSound;

    public override void EnterState(PlayerStateManager stateManager)
    {
        this.stateManager = stateManager;
        HandleAnimations();
        SetAnimationParameters();
        HandleAudio();
        LookAtCameraDirection();
        SpawnProjectile();
        stateManager.healthSystem.ConsumeStamina(stateManager.lightAttackStamCost);
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
    }

    public override void HandleAnimations()
    {
        stateManager.animator.Play("Light Attack");
    }

    public override void SetAnimationParameters()
    {
        stateManager.animator.speed = 1f;
        stateManager.animator.SetBool("IsMoving", false);
        stateManager.animator.SetBool("IsSprinting", false);
        stateManager.animator.SetBool("IsDodging", false);
        stateManager.animator.SetFloat("HorizontalMovement", 0);
        stateManager.animator.SetFloat("VerticalMovement", 0);
    }

    public override void HandleAudio()
    {
        stateManager.playerAudio.clip = shootSound;
        stateManager.playerAudio.loop = loopSound;
        stateManager.playerAudio.Play();
    }

    void LookAtCameraDirection()
    {
        stateManager.animator.transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
    }

    public void SpawnProjectile()
    {
        Vector3 position = new Vector3(stateManager.transform.position.x, stateManager.transform.position.y + 2.25f, stateManager.transform.position.z);
        GameObject newProjectile = Object.Instantiate(stateManager.projectile, position, stateManager.projectile.transform.rotation);

        GameObject target = GameObject.Find("LockOn Point R");

        if (stateManager.isLockedOn)
        {
            newProjectile.transform.up = (target.transform.position - newProjectile.transform.position).normalized;
            newProjectile.GetComponent<PlayerProjectile>().InitialiseArrowValues(stateManager.gameObject, target, stateManager.lightAttackDamage, stateManager.lightAttackGemRecharge, stateManager.lockedOnArrowTrackingStrength);
        }
        else
        {
            newProjectile.transform.up = stateManager.animator.transform.forward;
            newProjectile.GetComponent<PlayerProjectile>().InitialiseArrowValues(stateManager.gameObject, target, stateManager.lightAttackDamage, stateManager.lightAttackGemRecharge, stateManager.arrowTrackingStrength);
        }

    }

    public override void OnCollisionEnter(Collider collider)
    {

    }

    public override void OnCollisionExit(Collider collider)
    {

    }
}