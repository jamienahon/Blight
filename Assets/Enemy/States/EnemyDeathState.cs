using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class EnemyDeathState : EnemyState
{
    public override EnemyStateManager stateManager { get; set; }
   // public AudioSource Dying;


    public override void EnterState(EnemyStateManager stateManager)
    {
        this.stateManager = stateManager;
        HandleAnimations();
    }

    public override void UpdateState()
    {
    }

    public override void HandleAnimations()
    {
        stateManager.animator.Play("Death");
        //Dying.Play();
        stateManager.enabled = false;
    }

    public override void SetAnimationParameters()
    {

    }

    public override void HandleAudio()
    {

    }

    public override void OnTriggerEnter(Collider collider)
    {

    }

    public override void OnTriggerExit(Collider collider)
    {

    }
}