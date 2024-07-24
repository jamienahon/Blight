using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerState
{
    public override void EnterState(PlayerStateManager stateManager)
    {
        stateManager.animator.Play("Unarmed-DiveRoll-Forward1");
    }

    public override void UpdateState(PlayerStateManager stateManager)
    {
        stateManager.transform.Translate(stateManager.animator.gameObject.transform.forward * stateManager.moveSpeed * Time.deltaTime);
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
