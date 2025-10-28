using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Enemy : MonoBehaviour
{
    public float health = 30f;
    public float moveSpeed = 2f;
    public Transform targetPoint; // 목표 지점 (예: 플레이어, 베이스 등)
    public GameObject train;
    public float fireRate = 3f;
    public Transform firePoint;
    public GameObject bulletPrefab;

    public Transform movePoint;
    public EnemyData enemyData;

    private float fireTimer = 0f;

    public void SetTarget(Transform TargetPoint, Transform MovePoint, float speed)
    {
        targetPoint = TargetPoint;
        movePoint = MovePoint;
        moveSpeed = speed;
    }

    private void Start()
    {
        train = GameObject.Find("Train");
    }
    void Update()
    {
        if (targetPoint == null) return;

        Vector3 dir = (targetPoint.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;

        if (movePoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
    public void AttackTrain()
    {
        if (train == null) return;

        fireTimer += Time.deltaTime;
        if (fireTimer >= 1f / fireRate)
        {
            fireTimer = 0f;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            transform.position = train.transform.position;
            
        }
    }
}
