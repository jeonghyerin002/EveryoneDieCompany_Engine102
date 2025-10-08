using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class TurretController : MonoBehaviour
{
    [Header("타겟 탐지 설정")]
    public float detectionRange = 10f;   // 포탑이 적을 감지하는 거리
    public string enemyTag = "Enemy";    // 적 태그

    [Header("회전 & 발사 설정")]
    public float rotationSpeed = 5f;     // 회전 속도
    public float fireRate = 1f;          // 초당 발사 속도
    public GameObject bulletPrefab;      // 발사체 프리팹
    public Transform firePoint;          // 총알이 나갈 위치

    [Header("탄약")]
    public int currentBullets = 0;

    private float fireTimer = 0f;
    private Transform currentTarget;

    void Update()
    {
        FindTarget();
        RotateToTarget();
        FireToTarget();
    }
    public void AddBullets(int amount)
    {
        currentBullets += amount;
    }
    void Shoot()
    {
        if (currentBullets <= 0)
        {
            return;
        }
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        currentBullets--;
    }

    // ?? 주변에서 가장 가까운 적 찾기
    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float closestDistance = Mathf.Infinity;
        Transform nearest = null;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < closestDistance && dist <= detectionRange)
            {
                closestDistance = dist;
                nearest = enemy.transform;
            }
        }

        currentTarget = nearest;
    }

    // ?? 타겟 방향으로 부드럽게 회전
    void RotateToTarget()
    {
        if (currentTarget == null) return;

        Vector3 direction = currentTarget.position - transform.position;
        direction.y = 0; // 수평 회전만

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    // ?? 타겟 향해 발사
    void FireToTarget()
    {
        if (currentTarget == null) return;

        fireTimer += Time.deltaTime;
        if (fireTimer >= 1f / fireRate)
        {
            fireTimer = 0f;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetTarget(currentTarget);
        }
    }

    // 시각적 감지 범위 표시
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
