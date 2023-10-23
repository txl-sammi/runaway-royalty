using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilMinionSpawner : MonoBehaviour
{
    //TODO: spawn position should be random
    //TODO: check memory leak warning???
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float startSpawnAfterGameStart;
    [SerializeField] private int maxEnemyCount;
    private GameObject target;
    [SerializeField]private int groupSize = 3;
    [SerializeField]private float spawnInterval = 5f;
    private float spawnDistance = 35f;
    private float spawnRadius = 10;

    private float timer = 0f;
    private float gameStart = 0;
    private Vector3 spawnArea = new Vector3(0,0,0);

    [SerializeField] private float gameDifficulty = 1f;


    void Awake(){
        target = GameObject.FindGameObjectWithTag("Carriage");
    }


    void Update()
    {            
        Vector3 targetPosition = target.transform.position;
        spawnArea = targetPosition - new Vector3 (spawnDistance,0,0);

        gameStart += Time.deltaTime;
        timer += Time.deltaTime;
        if (gameStart >= startSpawnAfterGameStart && transform.childCount < maxEnemyCount){
            Spawn();
        }

        gameDifficulty += Time.deltaTime * 0.01f;
        
    }

    private void Spawn(){
        
        if (timer >= spawnInterval)
        {
            timer = 0f;

            Debug.Log("Spawn");



            for (int i = 0; i < groupSize; i++)
            {
                // Spawn in a random point around "spawnArea" in the XZ plane
                Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
                Vector3 spawnPoint = spawnArea + new Vector3(randomPoint.x, 0, randomPoint.y);

                GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
                var enemy = Instantiate(randomEnemy, spawnPoint, Quaternion.identity);
                enemy.transform.parent = gameObject.transform;
                
            }
        }
    }

        void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(spawnArea, spawnRadius);

    }

}
