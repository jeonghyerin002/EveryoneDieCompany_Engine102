using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("���� ������")]
    public TrainData trainData;
    public PlayerData playerData;
    public StageData currentStage;

    [Header("���� ����")]
    private bool isGameRunning = false;
    private float currentDistance = 0f;
    private float trainSpeed = 5f;

    [Header("UI")]
    public Slider distanceBar;

    [Header("�ӵ� �ν�Ʈ")]
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
        //���� �ʱ�ȭ
        trainData.maxHP = trainData.baseMaxHP + (playerData.hpUpgradeLevel * trainData.hpPerUpgrade);
        trainData.maxCoal = trainData.baseMaxCoal + (playerData.coalCapacityLevel * trainData.coalPerUpgrade);
        trainData.maxCannonBalls = trainData.baseMaxCannonBalls + (playerData.cannonCapacityLevel * trainData.cannonPerUpgrade);

        trainData.currentHP = trainData.maxHP;
        trainData.currentCoal = trainData.maxCoal;
        trainData.currentCannonBalls = trainData.maxCannonBalls;

        //�ӵ� ���
        trainSpeed = trainData.baseSpeed + (playerData.speedUpgradeLevel * trainData.speedPerUpgrade); 

        //�ʱ�ȭ
        currentDistance = 0f;
        isGameRunning = true;

        //���α׷����� �ʱ�ȭ
        if (distanceBar != null)
        {
            distanceBar.minValue = 0f;
            distanceBar.maxValue = 1f;
            distanceBar.value = 0f;
        }
        Debug.Log("���� ����");
    }
    void UpdateGame()
    {
        //�Ÿ� ����
        currentDistance += trainSpeed * Time.deltaTime;

        //���� üũ
        if (currentDistance >= currentStage.stageDistance)
        {
            StageComplete();
        }

        //���� ���� üũ
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
                Debug.Log("�Ӻ� �ν�Ʈ ����!");
            }
        }
    }
    void StageComplete()
    {
        isGameRunning = false;

        //����
        playerData.gold += currentStage.baseGoldReward;
        playerData.currentExp += currentStage.baseExpReward;

        //������
        while (playerData.currentExp >= playerData.expToNextLevel)
        {
            playerData.currentExp -= playerData.expToNextLevel;
            playerData.playerLevel++;
            playerData.expToNextLevel = playerData.playerLevel * 100;
        }
        Debug.Log("�������� �Ϸ�! ��� :" + playerData.gold);
    }
    void GameOver()
    {
        isGameRunning = false;
        Debug.Log("���� ����!");
    }
    public void TakeDamage(int damage)
    {
        trainData.currentHP -= damage;
        Debug.Log("������! ���� Hp" + trainData.currentHP);
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
