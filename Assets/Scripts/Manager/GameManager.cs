using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("게임 데이터")]
    public TrainData trainData;
    public PlayerData playerData;
    public StageData currentStage;

    [Header("게임 상태")]
    private bool isGameRunning = false;
    private float currentDistance = 0f;
    private float trainSpeed = 5f;

    [Header("UI")]
    public Slider distanceBar;
    public Slider hpBar; // 열차 HP바

    [Header("속도 부스트")]
    private float originalSpeed = 0f;
    private float boostTimer = 0f;
    private bool isBoosting = false;

    [Header("석탄 시스템")]
    public CoalInteraction coalStorage;

    void Awake()
    {
        playerData = new PlayerData();
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        InitGame();
    }

    void Update()
    {
        if (isGameRunning)
        {
            UpdateGame();
            UpdateDistanceBar();
            UpdateSpeedBoost();
        }
    }

    void InitGame()
    {
        // 1) 파트 보너스 합계 새로 계산
        PlayerPartInventory.Instance.RecalcPartBonuses();
        var inv = PlayerPartInventory.Instance;

        // 2) 업그레이드 + 파트 보너스 합산
        trainData.maxHP = trainData.baseMaxHP
                                  + (playerData.hpUpgradeLevel * trainData.hpPerUpgrade)
                                  + inv.partHPBonusSum;

        trainData.maxCoal = trainData.baseMaxCoal
                                  + (playerData.coalCapacityLevel * trainData.coalPerUpgrade)
                                  + inv.partCoalBonusSum;

        trainData.maxCannonBalls = trainData.baseMaxCannonBalls
                                  + (playerData.cannonCapacityLevel * trainData.cannonPerUpgrade)
                                  + inv.partAmmoBonusSum;

        float speedFromUpgrade = (playerData.speedUpgradeLevel * trainData.speedPerUpgrade);
        float speedFromParts = inv.partSpeedBonusSum;
        trainData.currentHP = trainData.maxHP;
        trainData.currentCoal = trainData.maxCoal;
        trainData.currentCannonBalls = trainData.maxCannonBalls;

        // 최종 속도
        trainSpeed = trainData.baseSpeed + speedFromUpgrade + speedFromParts;

        // 나머지 초기화/바 UI 등 기존 로직 유지
        currentDistance = 0f;
        isGameRunning = true;

        if (distanceBar != null)
        {
            distanceBar.minValue = 0f;
            distanceBar.maxValue = 1f;
            distanceBar.value = 0f;
        }

        // HP바 초기화
        UpdateHPBar();

        Debug.Log("게임 시작");
    }

    void UpdateGame()
    {
        // 석탄에 따른 속도 계산
        float currentSpeed = GetCurrentSpeed();

        // 거리 진행
        currentDistance += currentSpeed * Time.deltaTime;

        // 도착 체크
        if (currentDistance >= currentStage.stageDistance)
        {
            StageComplete();
        }

        // 게임 오버 체크
        if (trainData.currentHP <= 0)
        {
            GameOver();
        }
    }

    float GetCurrentSpeed()
    {
        if (coalStorage == null || coalStorage.currentCoal <= 0)
        {
            return 0f; // 석탄 없으면 멈춤
        }

        // 석탄 비율에 따른 속도 계산
        float coalRatio = (float)coalStorage.currentCoal / coalStorage.maxCoal;
        return trainSpeed * coalRatio;
    }

    void UpdateDistanceBar()
    {
        if (distanceBar != null)
        {
            float progress = currentDistance / currentStage.stageDistance;
            distanceBar.value = progress;
        }
    }

    void UpdateHPBar()
    {
        if (hpBar != null)
        {
            hpBar.value = (float)trainData.currentHP / trainData.maxHP;
        }
    }

    void UpdateSpeedBoost()
    {
        if (isBoosting)
        {
            boostTimer -= Time.deltaTime;

            if (boostTimer <= 0f)
            {
                trainSpeed = originalSpeed;
                isBoosting = false;
                Debug.Log("속보 부스트 종료!");
            }
        }
    }

    void StageComplete()
    {
        isGameRunning = false;

        // 보상
        playerData.gold += currentStage.baseGoldReward;
        playerData.currentExp += currentStage.baseExpReward;

        // 레벨업
        while (playerData.currentExp >= playerData.expToNextLevel)
        {
            playerData.currentExp -= playerData.expToNextLevel;
            playerData.playerLevel++;
            playerData.expToNextLevel = playerData.playerLevel * 100;
        }
        Debug.Log("스테이지 완료! 골드 :" + playerData.gold);

        int i = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = i + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void GameOver()
    {
        isGameRunning = false;
        Debug.Log("게임 오버!");
    }

    public void TakeDamage(int damage)
    {
        trainData.currentHP -= damage;
        trainData.currentHP = Mathf.Max(trainData.currentHP, 0); // 음수 방지
        UpdateHPBar(); // HP바 업데이트
        Debug.Log("데미지! 현재 HP: " + trainData.currentHP);
    }

    public float GetDistanceProgress()
    {
        return currentDistance / currentStage.stageDistance;
    }

    public void StartSpeedBoost(float boostAmount, float duration)
    {
        if (!isBoosting)
        {
            originalSpeed = trainSpeed;
        }
        trainSpeed += boostAmount;
        boostTimer = duration;
        isBoosting = true;
    }
}