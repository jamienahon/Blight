using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerState
{
    public override void EnterState(PlayerStateManager stateManager)
    {
        Debug.Log("dodged");
    }

    public override void UpdateState(PlayerStateManager stateManager)
    {
        stateManager.SwitchState(stateManager.idleState);
    }

    public override void HandleInputs(PlayerStateManager stateManager)
    {
        
    }

    public override void OnCollisionEnter(PlayerStateManager stateManager, Collision collision)
    {
        
    }

    public override void OnCollisionExit(PlayerStateManager stateManager, Collision collision)
    {
        
    }
}
