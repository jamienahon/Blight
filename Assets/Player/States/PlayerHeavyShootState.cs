using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.FilePathAttribute;

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
        //SpawnMultiProjectile();
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
        for (int rotation = -45; rotation <= 45; rotation += 45)
        {
            Vector3 position = new Vector3(stateManager.transform.position.x, stateManager.transform.position.y + 2.25f, stateManager.transform.position.z);
            GameObject newProjectile = Object.Instantiate(stateManager.projectile, position, stateManager.projectile.transform.rotation);

            GameObject target = null;
            if (stateManager.isLockedOn)
            {
                target = Camera.main.gameObject.GetComponent<CameraController>().currentLockOnPoint.gameObject;
                newProjectile.transform.up = Quaternion.Euler(0, rotation, 0) * (target.transform.position - newProjectile.transform.position).normalized;
            }
            else
                newProjectile.transform.up = Quaternion.Euler(0, rotation, 0) * stateManager.animator.transform.forward;

            newProjectile.GetComponent<PlayerProjectile>().InitialiseArrowValues(stateManager.gameObject, target, stateManager.lightAttackDamage, stateManager.lightAttackGemRecharge);
        }
    }

    public override void OnCollisionEnter(Collider collider)
    {

    }

    public override void OnCollisionExit(Collider collider)
    {

    }
}
