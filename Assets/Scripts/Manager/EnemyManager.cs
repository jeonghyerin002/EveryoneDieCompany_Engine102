using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemySO enemySO;
    public float spawnInterval = 5f;
    public float spawnRange = 5f;
    public int maxEnemyCount;

    public float timer = 0f;
    void Start()
    {

    }


    void Update()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        timer = Time.deltaTime;

        if(timer >= spawnInterval)
        {
            Vector3 spawnPos = new Vector3(
                transform.position.x + Random.Range(-spawnRange, spawnRange),
                transform.position.y,
                transform.position.z + Random.Range(-spawnRange, spawnRange)
                );

            Instantiate(enemySO.enemyPrefab, spawnPos, Quaternion.identity);
            timer = 0f;
        }
    }
}
