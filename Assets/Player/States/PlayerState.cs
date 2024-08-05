using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    public abstract PlayerStateManager stateManager { get; set; }

    public abstract void EnterState(PlayerStateManager stateManager);

    public abstract void UpdateState();

    public abstract void HandleInputs();

    public abstract void HandleAnimations();

    public abstract void SetAnimationParameters();

    public abstract void OnCollisionEnter(Collider collider);

    public abstract void OnCollisionExit(Collider collider);
}
