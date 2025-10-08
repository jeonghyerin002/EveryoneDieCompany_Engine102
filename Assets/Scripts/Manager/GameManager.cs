using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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

    [Header("속도 부스트")]
    private float originalSpeed = 0f;
    private float boostTimer = 0f;
    private bool isBoosting = false;

    void Awake()
    {
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
        //열차 초기화
        trainData.maxHP = trainData.baseMaxHP + (playerData.hpUpgradeLevel * trainData.hpPerUpgrade);
        trainData.maxCoal = trainData.baseMaxCoal + (playerData.coalCapacityLevel * trainData.coalPerUpgrade);
        trainData.maxCannonBalls = trainData.baseMaxCannonBalls + (playerData.cannonCapacityLevel * trainData.cannonPerUpgrade);

        trainData.currentHP = trainData.maxHP;
        trainData.currentCoal = trainData.maxCoal;
        trainData.currentCannonBalls = trainData.maxCannonBalls;

        //속도 계산
        trainSpeed = trainData.baseSpeed + (playerData.speedUpgradeLevel * trainData.speedPerUpgrade); 

        //초기화
        currentDistance = 0f;
        isGameRunning = true;

        //프로그래스바 초기화
        if (distanceBar != null)
        {
            distanceBar.minValue = 0f;
            distanceBar.maxValue = 1f;
            distanceBar.value = 0f;
        }
        Debug.Log("게임 시작");
    }
    void UpdateGame()
    {
        //거리 진행
        currentDistance += trainSpeed * Time.deltaTime;

        //도착 체크
        if (currentDistance >= currentStage.stageDistance)
        {
            StageComplete();
        }

        //게임 오버 체크
        if (trainData.currentHP <= 0)
        {
            GameOver();
        }
    }
    void UpdateDistanceBar()
    {
        if (distanceBar != null)
        {
            float progress = currentDistance / currentStage.stageDistance;
            distanceBar.value = progress;
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

        //보상
        playerData.gold += currentStage.baseGoldReward;
        playerData.currentExp += currentStage.baseExpReward;

        //레벨업
        while (playerData.currentExp >= playerData.expToNextLevel)
        {
            playerData.currentExp -= playerData.expToNextLevel;
            playerData.playerLevel++;
            playerData.expToNextLevel = playerData.playerLevel * 100;
        }
        Debug.Log("스테이지 완료! 골드 :" + playerData.gold);
    }
    void GameOver()
    {
        isGameRunning = false;
        Debug.Log("게임 오버!");
    }
    public void TakeDamage(int damage)
    {
        trainData.currentHP -= damage;
        Debug.Log("데미지! 현재 Hp" + trainData.currentHP);
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
