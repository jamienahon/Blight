using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AudioSetting
{
    public AudioClip audioClip;
    public bool loop;
}

public class AudioManager : MonoBehaviour
{
    [Header("References")]
    public PlayerStateManager playerStateManager;
    public EnemyStateManager enemyStateManager;

    [Header("Player")]
    public AudioSetting playerWalking;
    public AudioSetting playerSprinting;
    public AudioSetting playerDodging;
    public AudioSetting playerLightAttack;
    public AudioSetting playerHeavyAttack;

    [Header("Enemy")]
    public AudioSetting enemyWalking;
    public AudioSetting enemyAttack;

    private void Start()
    {
        playerStateManager.walkState.walkingSound = playerWalking.audioClip;
        playerStateManager.walkState.loopSound = playerWalking.loop;

        playerStateManager.sprintState.sprintSound = playerSprinting.audioClip;
        playerStateManager.sprintState.loopSound = playerSprinting.loop;

        playerStateManager.dodgeState.dodgeSound = playerDodging.audioClip;
        playerStateManager.dodgeState.loopSound = playerDodging.loop;

        playerStateManager.shootState.shootSound = playerLightAttack.audioClip;
        playerStateManager.shootState.loopSound = playerLightAttack.loop;

        playerStateManager.heavyShoot.heavyShootSound = playerHeavyAttack.audioClip;
        playerStateManager.heavyShoot.loopSound = playerHeavyAttack.loop;

        enemyStateManager.moveState.moveSound = enemyWalking.audioClip;
        enemyStateManager.moveState.loopSound = enemyWalking.loop;

        enemyStateManager.attackState.attackSound = enemyAttack.audioClip;
        enemyStateManager.attackState.loopSound = enemyAttack.loop;
    }
}
