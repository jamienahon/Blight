using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    public abstract void EnterState(PlayerStateManager stateManager);

    public abstract void UpdateState(PlayerStateManager stateManager);

    public abstract void HandleInputs(PlayerStateManager stateManager);

    public abstract void HandleAnimations(PlayerStateManager stateManager);

    public abstract void OnCollisionEnter(PlayerStateManager stateManager, Collision collision);

    public abstract void OnCollisionExit(PlayerStateManager stateManager, Collision collision);
}
