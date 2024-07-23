using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public override void EnterState(PlayerStateManager stateManager)
    {

    }

    public override void UpdateState(PlayerStateManager stateManager)
    {
        HandleInputs(stateManager);
    }

    public override void HandleInputs(PlayerStateManager stateManager)
    {
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            stateManager.SwitchState(stateManager.idleState);
    }

    public override void OnCollisionEnter(PlayerStateManager stateManager, Collision collision)
    {
        
    }

    public override void OnCollisionExit(PlayerStateManager stateManager, Collision collision)
    {
        
    }
}
