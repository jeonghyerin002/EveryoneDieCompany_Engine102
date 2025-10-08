using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("���� ����")]
    public List<EnemyData> enemyDataList;
    public float spawnInterval = 3f;

    [Header("�̵� ���")]
    public EnemyPath[] paths = new EnemyPath[8];  //��� 8��

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

        //���� ��� ����
        EnemyPath randomPath = paths[Random.Range(0, paths.Length)];
        if (randomPath == null || randomPath.startPoint == null || randomPath.endPoint == null) return;

        //���� �� ����
        EnemyData randomEnemyData = enemyDataList[Random.Range(0, enemyDataList.Count)];
        if (randomEnemyData == null || randomEnemyData.enemyPrefab == null) return;

        //��������� �� ����
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

            //�����
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(paths[i].endPoint.position, 0.5f);

            //������
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(paths[i].endPoint.position, 0.5f);

            //��μ�
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(paths[i].startPoint.position, paths[i].endPoint.position);

            //ȭ��ǥ (����ǥ��)
            Vector3 direction = (paths[i].endPoint.position - paths[i].startPoint.position).normalized;
            Vector3 arrawPos = paths[i].startPoint.position + direction * Vector3.Distance(paths[i].startPoint.position, paths[i].endPoint.position) * 0.5f;
            Gizmos.DrawSphere(arrawPos, 0.2f);
        }
    }
}
