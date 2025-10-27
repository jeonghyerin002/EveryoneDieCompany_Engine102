using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Enemy : MonoBehaviour
{
    public float health = 30f;
    public float moveSpeed = 2f;
    public Transform targetPoint; // 목표 지점 (예: 플레이어, 베이스 등)

    public Transform movePoint;

    public void SetTarget(Transform TargetPoint, Transform MovePoint, float speed)
    {
        targetPoint = TargetPoint;
        movePoint = MovePoint;
        moveSpeed = speed;
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
}
