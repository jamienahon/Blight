using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public PlayerStateManager stateManager;

    public void EndDodge()
    {
        stateManager.SwitchState(stateManager.idleState);
    }
}
