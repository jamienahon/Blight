using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    public abstract EnemyStateManager stateManager { get; set; }

    public abstract void EnterState(EnemyStateManager stateManager);

    public abstract void UpdateState();

    public abstract void HandleAnimations();

    public abstract void SetAnimationParameters();

    public abstract void HandleAudio();

    public abstract void OnTriggerEnter(Collider collider);

    public abstract void OnTriggerExit(Collider collider);
}
