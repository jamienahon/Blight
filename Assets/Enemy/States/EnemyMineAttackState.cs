using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMineAttackState : EnemyState
{
    public override EnemyStateManager stateManager { get; set; }

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
        stateManager.animator.Play("MineAttack");
    }

    public override void SetAnimationParameters()
    {

    }

    public override void HandleAudio()
    {

    }

    public void SpawnMines()
    {
        for(int i = 0; i < stateManager.numberOfMines; i++)
        {
            float xMin = stateManager.gameObject.transform.position.x - stateManager.mineSpawnRange;
            float xMax = stateManager.gameObject.transform.position.x + stateManager.mineSpawnRange;
            float zMin = stateManager.gameObject.transform.position.z - stateManager.mineSpawnRange;
            float zMax = stateManager.gameObject.transform.position.z + stateManager.mineSpawnRange;

            Vector3 position = new Vector3(Random.Range(xMin, xMax), 35.042f, Random.Range(zMin, zMax));
            GameObject newMine = Object.Instantiate(stateManager.minePrefab);
            newMine.transform.position = position;

            Mine mine = newMine.GetComponent<Mine>();
            mine.damage = stateManager.mineDamage;
        }
    }

    public override void OnTriggerEnter(Collider collider)
    {

    }

    public override void OnTriggerExit(Collider collider)
    {

    }
}
