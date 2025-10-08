using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("스폰 설정")]
    public List<EnemyData> enemyDataList;
    public float spawnInterval = 3f;

    [Header("이동 경로")]
    public EnemyPath[] paths = new EnemyPath[8];  //경로 8개

    private float spawnTimer = 0f;
    
    void Start()
    {
        
    }


    void Update()
    {
        spawnTimer += Time.deltaTime;
        
        if(spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }
    void SpawnEnemy()
    {
        if (enemyDataList == null || enemyDataList.Count == 0) return;
        if (paths.Length == 0 ) return;

        //랜덤 경로 선택
        EnemyPath randomPath = paths[Random.Range(0, paths.Length)];
        if (randomPath == null || randomPath.startPoint == null || randomPath.endPoint == null) return;

        //랜덤 적 선택
        EnemyData randomEnemyData = enemyDataList[Random.Range(0, enemyDataList.Count)];
        if (randomEnemyData == null || randomEnemyData.enemyPrefab == null) return;

        //출발지에서 적 생성
        GameObject enemy = Instantiate(randomEnemyData.enemyPrefab, randomPath.startPoint.position, Quaternion.identity);
        Enemy enemyScript = enemy.GetComponent<Enemy>();

        if (enemyScript != null )
        {
            enemyScript.SetTarget(randomPath.endPoint, randomEnemyData.moveSpeed);
            enemyScript.health = randomEnemyData.health;
        }
    }
    void OnDarwGizmos()
    {
        if (paths == null || paths.Length == 0) return;

        for (int i = 0; i < paths.Length; i++)
        {
            if (paths[i] == null) continue;
            if (paths[i].startPoint == null || paths[i].endPoint == null) continue;

            //출발지
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(paths[i].endPoint.position, 0.5f);

            //도착지
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(paths[i].endPoint.position, 0.5f);

            //경로선
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(paths[i].startPoint.position, paths[i].endPoint.position);

            //화살표 (방향표시)
            Vector3 direction = (paths[i].endPoint.position - paths[i].startPoint.position).normalized;
            Vector3 arrawPos = paths[i].startPoint.position + direction * Vector3.Distance(paths[i].startPoint.position, paths[i].endPoint.position) * 0.5f;
            Gizmos.DrawSphere(arrawPos, 0.2f);
        }
    }
}
