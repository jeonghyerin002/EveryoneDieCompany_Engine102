using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 30f;
    public float moveSpeed = 2f;
    public Transform targetPoint; // 목표 지점 (예: 플레이어, 베이스 등)

    void Update()
    {
        if (targetPoint == null) return;

        Vector3 dir = (targetPoint.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
