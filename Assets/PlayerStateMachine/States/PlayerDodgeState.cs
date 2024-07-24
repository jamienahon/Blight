using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerState
{
    public float y;
    public override void EnterState(PlayerStateManager stateManager)
    {
        stateManager.animator.SetBool("IsDodging", true);
        Vector3 movDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 dodgeDirection = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * movDirection;
        stateManager.animator.transform.rotation = Quaternion.LookRotation(dodgeDirection);

    }

    public override void UpdateState(PlayerStateManager stateManager)
    {
        stateManager.transform.Translate(stateManager.animator.transform.forward * stateManager.moveSpeed * Time.deltaTime);
    }

    public override void HandleInputs(PlayerStateManager stateManager)
    {
        
    }

    public override void HandleAnimations(PlayerStateManager stateManager)
    {
        
    }

    public override void OnCollisionEnter(PlayerStateManager stateManager, Collision collision)
    {
        
    }

    public override void OnCollisionExit(PlayerStateManager stateManager, Collision collision)
    {
        
    }
}
