using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public PlayerStateManager stateManager;

    public float moveSpeed;

    public Vector3 movementDirection = new Vector3();
    public float jumpHeight;
}
