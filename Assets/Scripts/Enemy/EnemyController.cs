using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class EnemyController : MonoBehaviour
{
    [Header("타겟 감지 설정")]
    public float detectionRange = 10f;     // 타겟을 인식할 최대 거리
    public string trainTag = "Train";      // 타겟 태그 이름

    [Header("회전 & 발사 설정")]
    public float rotationSpeed = 5f;       // 포신 회전 속도
    public float fireRate = 1f;            // 초당 발사 횟수
    public GameObject bulletPrefab;        // 총알 프리팹
    public Transform firePoint;            // 총알 발사 위치
    public Transform tankUp;               // 포신 오브젝트 (TankUP)

    [Header("상태")]
    public Transform target;               // 현재 조준 중인 타겟
    public bool isTank;                    // 탱크 여부

    private float fireTimer = 0f;          // 발사 주기 타이머

    void Update()
    {
        FindTarget();

        if (isTank && target != null)
        {
            RotateTankUpToTarget();
            FireToTarget();
        }
    }

    // 일정 거리 안의 타겟(Train) 탐색
    void FindTarget()
    {
        GameObject train = GameObject.FindGameObjectWithTag(trainTag);
        if (train == null)
        {
            target = null;
            return;
        }

        float dist = Vector3.Distance(transform.position, train.transform.position);
        target = (dist <= detectionRange) ? train.transform : null;
    }

    // TankUP 오브젝트가 타겟을 향하도록 회전
    void RotateTankUpToTarget()
    {
        if (target == null || tankUp == null) return;

        Vector3 dir = target.position - tankUp.position;
        dir.y = 0f; // 수평 회전만 적용

        Quaternion targetRot = Quaternion.LookRotation(dir);
        tankUp.rotation = Quaternion.Lerp(tankUp.rotation, targetRot, Time.deltaTime * rotationSpeed);
    }

    // 일정 시간마다 타겟을 향해 발사
    void FireToTarget()
    {
        if (target == null) return;

        fireTimer += Time.deltaTime;
        if (fireTimer < 1f / fireRate) return;
        fireTimer = 0f;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        bulletScript.damage = 10;                  // 기본 공격력
        bulletScript.SetTarget(target); // 총알 타겟 설정

        Debug.Log($"[{name}] → {target.name} 발사!");
    }
}
