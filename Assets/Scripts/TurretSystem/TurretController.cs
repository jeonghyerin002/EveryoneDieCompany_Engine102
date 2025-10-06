using UnityEngine;
using System.Collections.Generic;

public class TurretController : MonoBehaviour
{
    [Header("Ÿ�� Ž�� ����")]
    public float detectionRange = 10f;   // ��ž�� ���� �����ϴ� �Ÿ�
    public string enemyTag = "Enemy";    // �� �±�

    [Header("ȸ�� & �߻� ����")]
    public float rotationSpeed = 5f;     // ȸ�� �ӵ�
    public float fireRate = 1f;          // �ʴ� �߻� �ӵ�
    public GameObject bulletPrefab;      // �߻�ü ������
    public Transform firePoint;          // �Ѿ��� ���� ��ġ

    private float fireTimer = 0f;
    private Transform currentTarget;

    void Update()
    {
        FindTarget();
        RotateToTarget();
        FireToTarget();
    }

    // ?? �ֺ����� ���� ����� �� ã��
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

    // ?? Ÿ�� �������� �ε巴�� ȸ��
    void RotateToTarget()
    {
        if (currentTarget == null) return;

        Vector3 direction = currentTarget.position - transform.position;
        direction.y = 0; // ���� ȸ����

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    // ?? Ÿ�� ���� �߻�
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

    // �ð��� ���� ���� ǥ��
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
