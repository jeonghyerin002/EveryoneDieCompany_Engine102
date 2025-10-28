using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;
    public int damage = 10;

    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
        transform.forward = dir;
    }

    void OnTriggerEnter(Collider other)
    {
        // 열차에 맞으면
        if (other.CompareTag("Train"))
        {
            // GameManager를 통해 데미지
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TakeDamage(damage);
                Debug.Log("적 총알이 열차 명중! 데미지: " + damage);
            }

            // 이펙트 재생
            if (EffectManager.Instance != null)
            {
                EffectManager.Instance.PlayEffect("Hit", transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}