using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        SpawnMultiProjectile();
        stateManager.healthSystem.ConsumeStamina(stateManager.heavyAttackStamCost);
    }

    public override void UpdateState()
    {

    }

    public override void HandleInputs()
    {

    }

    public override void HandleAnimations()
    {
        stateManager.animator.Play("Heavy Attack");
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
        stateManager.playerAudio.clip = heavyShootSound;
        stateManager.playerAudio.loop = loopSound;
        stateManager.playerAudio.Play();
    }

    void LookAtCameraDirection()
    {
        stateManager.animator.transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
    }

    public void SpawnMultiProjectile()
    {
        Vector3 position = new Vector3(stateManager.transform.position.x, stateManager.transform.position.y + 2.25f, stateManager.transform.position.z);
        if (stateManager.isLockedOn)
        {
            for (int rotation = -45; rotation <= 45; rotation += 45)
            {
                GameObject newProjectile = Object.Instantiate(stateManager.projectile, position, stateManager.projectile.transform.rotation);
                PlayerProjectile arrowScript = newProjectile.GetComponent<PlayerProjectile>();
                arrowScript.target = Camera.main.gameObject.GetComponent<CameraController>().currentLockOnPoint.gameObject;
                arrowScript.player = stateManager.gameObject;
                arrowScript.trackingStrength = stateManager.arrowTrackingStrength;
                arrowScript.damage = stateManager.heavyAttackDamage / 3.0f;
                arrowScript.gemRechargeAmount = stateManager.heavyAttackGemRecharge / 3.0f;
                arrowScript.damageFalloff = stateManager.damageFalloff;

                Vector3 lockOnPos = Camera.main.gameObject.GetComponent<CameraController>().currentLockOnPoint.gameObject.transform.position;
                newProjectile.transform.up = (lockOnPos - newProjectile.transform.position).normalized;
                newProjectile.transform.up = Quaternion.Euler(0, rotation, 0) * newProjectile.transform.up;
            }
        }
        else
        {
            for (int rotation = -45; rotation <= 45; rotation += 45)
            {
                GameObject newProjectile = Object.Instantiate(stateManager.projectile, position, stateManager.projectile.transform.rotation);

                newProjectile.transform.up = stateManager.animator.transform.forward;
                newProjectile.transform.up = Quaternion.Euler(0, rotation, 0) * newProjectile.transform.up;
            }
        }
    }

    public override void OnCollisionEnter(Collider collider)
    {

    }

    public override void OnCollisionExit(Collider collider)
    {

    }
}
