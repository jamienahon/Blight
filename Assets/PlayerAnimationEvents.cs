using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public PlayerStateManager stateManager;

    public void StartDodge()
    {
        stateManager.isInvincible = true;
    }

    public void EndDodge()
    {
        stateManager.isInvincible = false;
        stateManager.SwitchState(stateManager.idleState);
        stateManager.animator.SetBool("IsDodging", false);
    }

    public void EndJump()
    {
        stateManager.jumpState.rb.useGravity = false;
        stateManager.SwitchState(stateManager.idleState);
        stateManager.animator.SetBool("IsJumping", false);
    }

    public void StartFalling()
    {
        stateManager.animator.speed = 0;
        stateManager.jumpState.isJumping = false;
    }

    public void EndLAttack()
    {
        stateManager.SwitchState(stateManager.idleState);
    }

    public void StartMove()
    {
        stateManager.hAttackState.move = true;
        stateManager.lAttackState.move = true;
    }

    public void EndMove()
    {
        stateManager.hAttackState.move = false;
        stateManager.lAttackState.move = false;
    }

    public void EndGetHit()
    {
        stateManager.SwitchState(stateManager.idleState);
    }
}
