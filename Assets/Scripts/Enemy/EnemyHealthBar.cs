using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("체력바 설정")]
    public Slider healthSlider;
    public RectTransform rectTransform;
    public Vector3 offset = new Vector3(0, 50f, 0); // 스크린 상 오프셋 (픽셀)
    public bool debugMode = false; // 디버그 모드

    private Enemy enemy;
    private Camera mainCamera;
    private Canvas parentCanvas;

    public void Initialize(Enemy targetEnemy, Canvas canvas)
    {
        enemy = targetEnemy;
        mainCamera = Camera.main;
        parentCanvas = canvas;

        if (mainCamera == null)
        {
            Debug.LogError("메인 카메라를 찾을 수 없습니다!");
        }

        if (healthSlider != null)
        {
            healthSlider.maxValue = 1f;
            healthSlider.value = 1f;
        }

        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }

        // 체력바를 켜진 상태로 시작
        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(true);
        }

        if (debugMode)
        {
            Debug.Log($"체력바 초기화 완료 - Enemy: {enemy.name}, Camera: {mainCamera.name}");
        }
    }

    void Update()
    {
        if (enemy == null)
        {
            Destroy(gameObject);
            return;
        }

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null) return;
        }

        // 적의 월드 위치를 스크린 위치로 변환
        Vector3 worldPos = enemy.transform.position;
        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);

        if (debugMode)
        {
            Debug.Log($"World: {worldPos}, Screen: {screenPos}");
        }

        // 화면 뒤에 있는지만 체크 (나머지는 표시)
        if (screenPos.z < 0)
        {
            if (healthSlider != null)
            {
                healthSlider.gameObject.SetActive(false);
            }
            return;
        }
        else
        {
            if (healthSlider != null)
            {
                healthSlider.gameObject.SetActive(true);
            }
        }

        // 스크린 위치에 오프셋 추가 (머리 위로)
        screenPos.y += offset.y;

        // RectTransform 위치 설정
        if (rectTransform != null)
        {
            rectTransform.position = screenPos;
        }

        // 체력바 값 업데이트
        if (healthSlider != null)
        {
            float maxHealth = enemy.enemyData != null ? enemy.enemyData.health : 30f;
            healthSlider.value = enemy.health / maxHealth;
        }
    }
}