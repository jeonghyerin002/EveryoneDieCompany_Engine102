using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("적 기본 설정")]
    public float health = 30f;
    public float moveSpeed = 2f;
    public int damage = 10;

    [Header("이동 설정")]
    public Transform targetPoint;
    public Transform movePoint;

    [Header("공격 설정")]
    public GameObject train;
    public float fireRate = 3f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    private float attackRange = 20f;

    [Header("데이터")]
    public EnemyData enemyData;

    [Header("UI")]
    public GameObject healthBarPrefab; // 체력바 프리팹
    public Canvas healthBarCanvas; // 체력바를 표시할 Canvas (Screen Space)
    private EnemyHealthBar healthBarInstance; // 생성된 체력바 인스턴스

    private float fireTimer = 0f;

    public void SetTarget(Transform TargetPoint, Transform MovePoint, float speed)
    {
        targetPoint = TargetPoint;
        movePoint = MovePoint;
        moveSpeed = speed;
    }

    private void Start()
    {
        FindClosestTrain();

        // Canvas 자동 찾기
        if (healthBarCanvas == null)
        {
            healthBarCanvas = FindObjectOfType<Canvas>();
        }

        CreateHealthBar(); // 체력바 생성
    }

    void Update()
    {
        // 열차를 잃어버렸으면 다시 찾기
        if (train == null)
        {
            FindClosestTrain();
        }

        // 이동 처리
        if (movePoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        }
        else if (targetPoint != null)
        {
            Vector3 dir = (targetPoint.position - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;
        }

        // 열차 공격
        if (train != null)
        {
            float distance = Vector3.Distance(transform.position, train.transform.position);
            if (distance <= attackRange)
            {
                AttackTrain();
            }
        }
    }

    void FindClosestTrain()
    {
        GameObject[] trains = GameObject.FindGameObjectsWithTag("Train");

        if (trains.Length == 0)
        {
            train = null;
            return;
        }

        GameObject closestTrain = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject t in trains)
        {
            float distance = Vector3.Distance(transform.position, t.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTrain = t;
            }
        }

        train = closestTrain;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0f)
        {
            DestroyHealthBar(); // 체력바 먼저 파괴
            Destroy(gameObject);
        }
    }

    void CreateHealthBar()
    {
        if (healthBarPrefab != null && healthBarCanvas != null)
        {
            GameObject healthBarObj = Instantiate(healthBarPrefab, healthBarCanvas.transform);
            healthBarInstance = healthBarObj.GetComponent<EnemyHealthBar>();

            if (healthBarInstance != null)
            {
                healthBarInstance.Initialize(this, healthBarCanvas);
            }
        }
    }

    void DestroyHealthBar()
    {
        if (healthBarInstance != null)
        {
            Destroy(healthBarInstance.gameObject);
        }
    }

    void OnDestroy()
    {
        DestroyHealthBar();
    }

    public void AttackTrain()
    {
        if (train == null) return;

        fireTimer += Time.deltaTime;

        if (fireTimer >= 1f / fireRate)
        {
            fireTimer = 0f;

            // 총알이 있으면 발사
            if (bulletPrefab != null && firePoint != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

                // EnemyBullet 컴포넌트가 있으면 타겟 설정
                EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
                if (enemyBullet != null)
                {
                    enemyBullet.SetTarget(train.transform);
                }
            }
            else
            {
                // 총알이 없으면 직접 데미지
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.TakeDamage(damage);
                    Debug.Log("적이 열차 공격! 데미지: " + damage);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}