using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UIElements;

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
        stateManager.transform.position = new Vector3(stateManager.transform.position.x, stateManager.transform.position.y + 0.75f, stateManager.transform.position.z);
        stateManager.animator.Play("Death");
        //Dying.Play();
        stateManager.enabled = false;
        stateManager.animator.gameObject.GetComponent<EnemyAnimationEvents>().enabled = false;
        Collider[] colliders = stateManager.gameObject.GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
            col.enabled = false;
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
