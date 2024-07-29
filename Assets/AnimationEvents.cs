using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public PlayerStateManager stateManager;

    public void EndDodge()
    {
        stateManager.SwitchState(stateManager.idleState);
        stateManager.animator.SetBool("IsDodging", false);
    }

    public void EndJump()
    {
        stateManager.jumpState.rb.useGravity = false;
        stateManager.SwitchState(stateManager.idleState);
    }

    public void StartFalling()
    {
        stateManager.animator.speed = 0;
        stateManager.jumpState.isJumping = false;
    }
}
