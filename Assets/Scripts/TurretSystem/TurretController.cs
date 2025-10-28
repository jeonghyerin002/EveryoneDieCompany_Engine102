using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class TurretController : MonoBehaviour
{
    [Header("타겟 탐지 설정")]
    public float detectionRange = 10f;
    public string enemyTag = "Enemy";

    [Header("회전 & 발사 설정")]
    public float rotationSpeed = 5f;
    public float fireRate = 1f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float fireTimer = 0f;
    private Transform currentTarget;
    private TurretInteraction turretInteraction;  // 추가

    void Start()
    {
        turretInteraction = GetComponent<TurretInteraction>();  // 추가
    }

    void Update()
    {
        FindTarget();
        RotateToTarget();
        FireToTarget();
    }

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

    void RotateToTarget()
    {
        if (currentTarget == null) return;

        Vector3 direction = currentTarget.position - transform.position;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void FireToTarget()
    {
        if (currentTarget == null) return;
        if (turretInteraction.currentBullets <= 0) return;  // 포탄 체크 추가

        fireTimer += Time.deltaTime;
        float effectiveFireRate = PlayerPartInventory.Instance.totalFireRate;

        if (fireTimer >= 1f / effectiveFireRate)
        {
            fireTimer = 0f;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.damage = Mathf.RoundToInt(PlayerPartInventory.Instance.totalDamage);
            bulletScript.SetTarget(currentTarget);

            turretInteraction.currentBullets--;  // 포탄 감소 추가
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}