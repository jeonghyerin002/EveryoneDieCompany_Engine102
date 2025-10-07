using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemySO enemySO;
    public float spawnInterval = 1f;
    public float spawnRange = 5f;
    public int maxEnemyCount;
    public float speed = 5f;

    private float timer = 0f;
    public Transform[] here;
    void Start()
    {
        
    }

    void Update()
    {
        SpawnEnemy();

        for (int i = 0; i < here.Length; i++)
        {
            if (here[i] != null)
            {
                int hereIndex = i;
                transform.position = here[i].position;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }


    void SpawnEnemy()
    {
        timer += Time.deltaTime;

        if(timer >= spawnInterval)
        {
            Vector3 spawnPos = new Vector3(
                transform.position.x + Random.Range(-spawnRange, spawnRange),
                transform.position.y,
                transform.position.z + Random.Range(-spawnRange, spawnRange)
                );
            Debug.Log("»ý¼º");
            Instantiate(enemySO.enemyPrefab, spawnPos, Quaternion.identity);
            timer = 0f;
        }
        
    }
}
