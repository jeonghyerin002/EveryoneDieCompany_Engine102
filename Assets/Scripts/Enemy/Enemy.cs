using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Enemy : MonoBehaviour
{
    public float health = 30f;
    public float moveSpeed = 2f;
    public Transform targetPoint; // ��ǥ ���� (��: �÷��̾�, ���̽� ��)

    public Transform target;

    public void SetTarget(Transform targetPoint, float speed)
    {
        target = targetPoint;
        moveSpeed = speed;
    }

    void Update()
    {
        if (targetPoint == null) return;

        Vector3 dir = (targetPoint.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;

        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
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
