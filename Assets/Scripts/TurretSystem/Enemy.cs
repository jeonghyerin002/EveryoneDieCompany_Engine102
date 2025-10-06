using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 30f;
    public float moveSpeed = 2f;
    public Transform targetPoint; // ��ǥ ���� (��: �÷��̾�, ���̽� ��)

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
