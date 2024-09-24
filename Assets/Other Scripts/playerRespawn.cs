using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRespawn : MonoBehaviour
{
    public float fallThreshold = -10f; // The Y value below which the player will respawn
    private Vector3 startPosition; // To store the starting position of the player

    void Start()
    {
        // Store the player's initial position when the game starts
        startPosition = transform.position;
    }

    void Update()
    {
        // Check if the player's Y position is below the threshold
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        // Reset the player's position to the starting position
        transform.position = startPosition;
    }
}
