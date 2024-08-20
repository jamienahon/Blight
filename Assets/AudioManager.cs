using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("References")]
    public PlayerStateManager playerStateManager;
    public EnemyStateManager enemyStateManager;

    [Header("Player")]
    public AudioClip playerWalking;
    public AudioClip playerSprinting;
    public AudioClip playerDodging;
    public AudioClip playerLightAttack;
    public AudioClip playerHeavyAttack;

    [Header("Enemy")]
    public AudioClip enemyWalking;
    public AudioClip enemyAttack;

    private void Start()
    {
        playerStateManager.walkState.walkingSound = playerWalking;
        playerStateManager.sprintState.sprintSound = playerSprinting;
        playerStateManager.dodgeState.dodgeSound = playerDodging;
        playerStateManager.shootState.shootSound = playerLightAttack;
        playerStateManager.heavyShoot.heavyShootSound = playerHeavyAttack;

        enemyStateManager.moveState.moveSound = enemyWalking;
        enemyStateManager.attackState.attackSound = enemyAttack;
    }
}
